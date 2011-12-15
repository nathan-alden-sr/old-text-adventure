using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

using Junior.Common;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using TextAdventure.Engine.Game;
using TextAdventure.Engine.Game.Commands;
using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Game.World;
using TextAdventure.WindowsGame.Configuration;
using TextAdventure.WindowsGame.Helpers;
using TextAdventure.WindowsGame.Managers;

namespace TextAdventure.WindowsGame.Components
{
	public class LogComponent : WindowComponent
	{
		private const int DetailIndent = 10;
		private const string LineFormatString = @"({0:h\:mm\:ss\.f})  {1}";
		private const Keys VisibilityToggleKey = Keys.L;
		private static readonly Dictionary<LogEntryType, Color> _shadowColors = new Dictionary<LogEntryType, Color>
		                                                                        	{
		                                                                        		{ LogEntryType.CommandExecutedSuccessfully, Color.Black },
		                                                                        		{ LogEntryType.CommandExecutionFailed, Color.Black },
		                                                                        		{ LogEntryType.EventRaising, Color.Black },
		                                                                        		{ LogEntryType.EventComplete, Color.Black },
		                                                                        		{ LogEntryType.EventCanceled, Color.Black },
		                                                                        		{ LogEntryType.EventHandled, Color.Black }
		                                                                        	};
		private static readonly Dictionary<LogEntryType, Color> _textColors = new Dictionary<LogEntryType, Color>
		                                                                      	{
		                                                                      		{ LogEntryType.CommandExecutedSuccessfully, Color.White },
		                                                                      		{ LogEntryType.CommandExecutionFailed, Color.Red },
		                                                                      		{ LogEntryType.EventRaising, Color.LightGreen },
		                                                                      		{ LogEntryType.EventComplete, Color.LightBlue },
		                                                                      		{ LogEntryType.EventCanceled, Color.PaleVioletRed },
		                                                                      		{ LogEntryType.EventHandled, Color.LightBlue }
		                                                                      	};
		private readonly KeyboardStateHelper _keyboardStateHelper;
		private readonly Queue<LogEntry> _logEntries;
		private readonly TimeSpan _logEntryLifetime;
		private readonly int _maximumVisibleLogLines;
		private readonly int? _minimumWindowWidth;
		private readonly bool _showTimestamps;
		private bool _visible;

		public LogComponent(GameManager gameManager)
			: base(gameManager)
		{
			BackgroundColor = Color.Black.WithAlpha(0.25f);
			_keyboardStateHelper = new KeyboardStateHelper(KeyDown, null, null, Keys.LeftControl, Keys.RightControl, VisibilityToggleKey);

			var configurationSection = (LogConfigurationSection)ConfigurationManager.GetSection("log");

			_visible = configurationSection.Visible;
			_maximumVisibleLogLines = configurationSection.MaximumVisibleLogLines;
			_minimumWindowWidth = configurationSection.MinimumWindowWidth;
			_logEntryLifetime = configurationSection.LogEntryLifetime;
			_showTimestamps = configurationSection.ShowTimestamps;
			_logEntries = new Queue<LogEntry>();

			DrawOrder = ComponentDrawOrder.Log;
		}

		public void EnqueueCommandExecutedLogEntry(TimeSpan loggedTotalGameTime, Command command, CommandResult result)
		{
			command.ThrowIfNull("command");

			LogEntryType entryType;

			switch (result)
			{
				case CommandResult.None:
				case CommandResult.Deferred:
					return;
				case CommandResult.Succeeded:
					entryType = LogEntryType.CommandExecutedSuccessfully;
					break;
				case CommandResult.Failed:
					entryType = LogEntryType.CommandExecutionFailed;
					break;
				default:
					throw new ArgumentOutOfRangeException("result");
			}

			var entry = new LogEntry(loggedTotalGameTime, command, entryType, _logEntryLifetime);

			EnqueueLogEntry(entry);
		}

		public void EnqueueEventRaisingLogEntry<TEvent>(TimeSpan loggedTotalGameTime, TEvent @event)
			where TEvent : Event
		{
			@event.ThrowIfNull("event");

			var entry = new LogEntry(loggedTotalGameTime, @event, LogEntryType.EventRaising, _logEntryLifetime);

			EnqueueLogEntry(entry);
		}

		public void EnqueueEventHandledLogEntry<TEvent>(TimeSpan loggedTotalGameTime, IEventHandler<TEvent> eventHandler, TEvent @event, EventResult result)
			where TEvent : Event
		{
			@event.ThrowIfNull("event");
			eventHandler.ThrowIfNull("eventHandler");

			var entry = new LogEntry(loggedTotalGameTime, eventHandler, result == EventResult.Complete ? LogEntryType.EventComplete : LogEntryType.EventCanceled, _logEntryLifetime);

			EnqueueLogEntry(entry);
		}

		public void ClearLog()
		{
			_logEntries.Clear();
		}

		public override void Update(GameTime gameTime)
		{
			_keyboardStateHelper.Update();

			DequeueOldLogEntries(gameTime);

			Visible = _visible && _logEntries.Any();
			if (Visible)
			{
				SetWindowRectangle();
			}

			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);

			SpriteFont spriteFont = FontContent.Calibri;
			Vector2 textVector = ClientRectangle.Location.ToVector2();
			int lineCount = 0;

			SpriteBatch.Begin();

			foreach (LogEntry logEntry in _logEntries)
			{
				Color textColor = _textColors[logEntry.EntryType];
				Color shadowColor = _shadowColors[logEntry.EntryType];

				logEntry.FadeHelper.Update(gameTime.TotalGameTime);

				string titleText = _showTimestamps ? String.Format(LineFormatString, logEntry.LoggedTotalGameTime, logEntry.Title) : logEntry.Title;

				SpriteBatch.DrawStringWithShadow(spriteFont, titleText, textVector, textColor * logEntry.FadeHelper.Alpha, shadowColor * logEntry.FadeHelper.Alpha, Vector2.One);

				if (++lineCount >= _maximumVisibleLogLines)
				{
					break;
				}

				textVector.X += DetailIndent;
				textVector.Y += spriteFont.LineSpacing;

				foreach (string detail in logEntry.Details)
				{
					SpriteBatch.DrawStringWithShadow(spriteFont, detail, textVector, textColor * logEntry.FadeHelper.Alpha, shadowColor * logEntry.FadeHelper.Alpha, Vector2.One);

					if (++lineCount >= _maximumVisibleLogLines)
					{
						break;
					}

					textVector.Y += spriteFont.LineSpacing;
				}

				if (lineCount >= _maximumVisibleLogLines)
				{
					break;
				}

				textVector.X -= DetailIndent;
			}

			SpriteBatch.End();
		}

		private void DequeueOldLogEntries(GameTime gameTime)
		{
			while (_logEntries.Count + _logEntries.Sum(arg => arg.Details.Count()) > _maximumVisibleLogLines ||
			       (_logEntries.Count + _logEntries.Sum(arg => arg.Details.Count()) > 0 && gameTime.TotalGameTime - _logEntries.Peek().LoggedTotalGameTime > _logEntryLifetime))
			{
				_logEntries.Dequeue();
			}
		}

		private void EnqueueLogEntry(LogEntry entry)
		{
			DequeueIfFull();

			if (_logEntryLifetime > TimeSpan.Zero)
			{
				_logEntries.Enqueue(entry);
			}
		}

		private void DequeueIfFull()
		{
			if (_logEntries.Count == _maximumVisibleLogLines)
			{
				_logEntries.Dequeue();
			}
		}

		private void SetWindowRectangle()
		{
			int maximumTitleTextWidth = _logEntries.Any() ? _logEntries.Max(arg => MeasureLineWidth(arg.LoggedTotalGameTime, arg.Title, 0, _showTimestamps)) : 0;
			int maximumDetailTextWidth = _logEntries.Any(arg => arg.Details.Any())
			                             	? _logEntries.Max(arg1 => arg1.Details.Any() ? arg1.Details.Max(arg2 => MeasureLineWidth(arg1.LoggedTotalGameTime, arg2, DetailIndent, false)) : 0)
			                             	: 0;
			int clientWidth = Math.Max(_minimumWindowWidth ?? 0, Math.Max(maximumTitleTextWidth, maximumDetailTextWidth));
			int lineCount = Math.Min(_maximumVisibleLogLines, _logEntries.Sum(arg => arg.LineCount));

			SetWindowRectangleUsingClientSize(Alignment.TopLeft, clientWidth, FontContent.Calibri.LineSpacing * lineCount);
		}

		private int MeasureLineWidth(TimeSpan loggedTotalGameTime, string text, int indent, bool showTimestamps)
		{
			if (showTimestamps)
			{
				text = String.Format(LineFormatString, loggedTotalGameTime, text);
			}

			return (int)Math.Ceiling(FontContent.Calibri.MeasureString(text).X + indent);
		}

		private void KeyDown(KeyboardState keyboardState, Keys keys)
		{
			_visible = keyboardState.AreKeysDown(Keys.LeftControl, VisibilityToggleKey) | keyboardState.AreKeysDown(Keys.RightControl, VisibilityToggleKey) ? !_visible : _visible;
		}

		private class LogEntry
		{
			private static readonly TimeSpan _fadeDuration = TimeSpan.FromMilliseconds(100);
			private readonly IEnumerable<string> _details;
			private readonly LogEntryType _entryType;
			private readonly FadeHelper _fadeHelper;
			private readonly TimeSpan _loggedTotalGameTime;
			private readonly string _title;

			public LogEntry(TimeSpan loggedTotalGameTime, ILoggable loggable, LogEntryType entryType, TimeSpan logEntryLifetime)
			{
				_loggedTotalGameTime = loggedTotalGameTime;
				_entryType = entryType;
				_title = loggable.Title;
				_details = loggable.Details.ToArray();
				_fadeHelper = new FadeHelper(FadeDirection.Out, loggedTotalGameTime + logEntryLifetime - _fadeDuration, _fadeDuration);
			}

			public string Title
			{
				get
				{
					return _title;
				}
			}

			public IEnumerable<string> Details
			{
				get
				{
					return _details;
				}
			}

			public int LineCount
			{
				get
				{
					return 1 + _details.Count();
				}
			}

			public TimeSpan LoggedTotalGameTime
			{
				get
				{
					return _loggedTotalGameTime;
				}
			}

			public LogEntryType EntryType
			{
				get
				{
					return _entryType;
				}
			}

			public FadeHelper FadeHelper
			{
				get
				{
					return _fadeHelper;
				}
			}
		}

		private enum LogEntryType
		{
			CommandExecutedSuccessfully,
			CommandExecutionFailed,
			EventRaising,
			EventComplete,
			EventCanceled,
			EventHandled
		}
	}
}