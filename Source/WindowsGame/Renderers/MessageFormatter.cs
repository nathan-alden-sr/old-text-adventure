using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Junior.Common;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

using Color = TextAdventure.Engine.Common.Color;

namespace TextAdventure.WindowsGame.Renderers
{
	public class MessageFormatter
	{
		private static readonly char[] _wordSeparator = new[] { ' ' };
		private readonly Dictionary<int, MessageTextAlignment> _alignmentsByLine = new Dictionary<int, MessageTextAlignment>
		                                                                           	{
		                                                                           		{ 0, MessageTextAlignment.Left }
		                                                                           	};
		private readonly Dictionary<Coordinate, Color> _colorsByWordCoordinate = new Dictionary<Coordinate, Color>
		                                                                         	{
		                                                                         		{ new Coordinate(0, 0), Color.White }
		                                                                         	};
		private readonly SpriteFont _font;
		private readonly Dictionary<int, Vector2> _lineSizesByLine = new Dictionary<int, Vector2>
		                                                             	{
		                                                             		{ 0, Vector2.Zero }
		                                                             	};
		private readonly float _maximumLineWidth;
		private readonly WindowTexture _selectedAnswerWindowTexture;
		private readonly MessageTextWord _spaceWord;
		private readonly Dictionary<int, List<MessageTextWord>> _wordsByLine = new Dictionary<int, List<MessageTextWord>>
		                                                                       	{
		                                                                       		{ 0, new List<MessageTextWord>() }
		                                                                       	};
		private IEnumerable<MessageAnswer> _answers = Enumerable.Empty<MessageAnswer>();

		public MessageFormatter(IMessage message, SpriteFont font, WindowTexture selectedAnswerWindowTexture, float maximumLineWidth)
		{
			message.ThrowIfNull("message");
			font.ThrowIfNull("font");
			selectedAnswerWindowTexture.ThrowIfNull("selectedAnswerWindowTexture");

			_font = font;
			_selectedAnswerWindowTexture = selectedAnswerWindowTexture;
			_maximumLineWidth = maximumLineWidth;
			_spaceWord = new MessageTextWord(" ", font.MeasureString(" "), false);

			ProcessMessageParts(message);
		}

		public MessageTextWord SpaceWord
		{
			get
			{
				return _spaceWord;
			}
		}

		public float MaximumLineWidthAfterFormatting
		{
			get
			{
				return _lineSizesByLine.Values.Any() ? _lineSizesByLine.Values.Max(arg => arg.X) : 0;
			}
		}

		public float TotalHeightAfterFormatting
		{
			get
			{
				return _lineSizesByLine.Values.Any() ? _lineSizesByLine.Values.Sum(arg => arg.Y) : 0;
			}
		}

		public int LineCount
		{
			get
			{
				return _wordsByLine.Keys.Count;
			}
		}

		public IEnumerable<MessageAnswer> Answers
		{
			get
			{
				return _answers;
			}
		}

		public IEnumerable<MessageTextWord> GetWordsByLine(int lineIndex)
		{
			return _wordsByLine[lineIndex].AsReadOnly();
		}

		public Vector2 GetLineSizeByLine(int lineIndex)
		{
			return _lineSizesByLine[lineIndex];
		}

		public MessageTextAlignment GetAlignmentByLine(int lineIndex)
		{
			return _alignmentsByLine[lineIndex];
		}

		public bool TryGetColorByWordCoordinate(Coordinate wordCoordinate, out Color color)
		{
			return _colorsByWordCoordinate.TryGetValue(wordCoordinate, out color);
		}

		private void ProcessMessageParts(IMessage message)
		{
			int lineIndex = 0;

			foreach (IMessagePart part in message.Parts)
			{
				var messageColor = part as MessageColor;
				var messageLineBreak = part as MessageLineBreak;
				var messageText = part as MessageText;
				var messageQuestion = part as MessageQuestion;

				if (messageColor != null)
				{
					ProcessColor(messageColor.Color, lineIndex);
				}
				else if (messageLineBreak != null)
				{
					ProcessLineBreak(ref lineIndex);
				}
				else if (messageText != null)
				{
					ProcessText(messageText.Text, MessageTextAlignment.Left, ref lineIndex);
				}
				else if (messageQuestion != null)
				{
					ProcessQuestion(messageQuestion, ref lineIndex);
				}
			}
		}

		private void ProcessColor(Color color, int lineIndex)
		{
			_colorsByWordCoordinate[new Coordinate(_wordsByLine[lineIndex].Count, lineIndex)] = color;
		}

		private void ProcessLineBreak(ref int lineIndex)
		{
			lineIndex++;
			_lineSizesByLine[lineIndex] = new Vector2(0, _spaceWord.Size.Y / 2);
			_wordsByLine[lineIndex] = new List<MessageTextWord>();
			_alignmentsByLine[lineIndex] = MessageTextAlignment.Left;
		}

		private void ProcessText(string text, MessageTextAlignment alignment, ref int lineIndex)
		{
			var lineText = new StringBuilder(String.Join(" ", _wordsByLine[lineIndex].Select(arg => arg.Text)));

			_lineSizesByLine[lineIndex] = _font.MeasureString(lineText);

			string[] words = text.Split(_wordSeparator);

			for (int i = 0; i < words.Length; i++)
			{
				string word = words[i];
				Vector2 wordSize = _font.MeasureString(word) + TextAdventure.Xna.Constants.MessageRenderer.ShadowOffset;
				Vector2 lineSize = _lineSizesByLine[lineIndex];

				lineSize.Y = Math.Max(wordSize.Y, lineSize.Y);
				_lineSizesByLine[lineIndex] = lineSize;

				if (i == 0)
				{
					if (lineSize.X + wordSize.X <= _maximumLineWidth)
					{
						lineText.Append(word);
						_lineSizesByLine[lineIndex] = new Vector2(lineSize.X + wordSize.X, lineSize.Y);
						_wordsByLine[lineIndex].Add(new MessageTextWord(word, wordSize, false));
						_alignmentsByLine[lineIndex] = alignment;
						continue;
					}

					lineIndex++;
					_lineSizesByLine[lineIndex] = Vector2.Zero;
					_wordsByLine[lineIndex] = new List<MessageTextWord>();
					_alignmentsByLine[lineIndex] = alignment;
				}

				if (lineSize.X + _spaceWord.Size.X + wordSize.X <= _maximumLineWidth)
				{
					if (lineText.Length > 0)
					{
						lineText.Append(' ');
					}
					lineText.Append(word);

					_lineSizesByLine[lineIndex] = new Vector2(lineSize.X + _spaceWord.Size.X + wordSize.X, lineSize.Y);
					_wordsByLine[lineIndex].Add(new MessageTextWord(word, wordSize, true));
				}
				else
				{
					lineText = new StringBuilder(word);
					lineIndex++;
					_lineSizesByLine[lineIndex] = new Vector2(wordSize.X, wordSize.Y);
					_wordsByLine[lineIndex] = new List<MessageTextWord>
					                          	{
					                          		new MessageTextWord(word, wordSize, false)
					                          	};
					_alignmentsByLine[lineIndex] = alignment;
				}
			}
		}

		private void ProcessQuestion(MessageQuestion question, ref int lineIndex)
		{
			if (!question.Answers.Any())
			{
				return;
			}
			if (_wordsByLine[lineIndex].Any())
			{
				ProcessLineBreak(ref lineIndex);
			}

			_answers = question.Answers;

			_colorsByWordCoordinate[new Coordinate(0, lineIndex)] = question.QuestionForegroundColor;

			ProcessText(question.Prompt, MessageTextAlignment.Center, ref lineIndex);
			ProcessLineBreak(ref lineIndex);

			MessageTextAnswer[] answers = question.Answers.Select(arg => GetMessageTextAnswer(question, arg)).ToArray();

			float lineWidth =
				(_selectedAnswerWindowTexture.SpriteWidth * 2 * answers.Length) +
				(TextAdventure.Xna.Constants.MessageRenderer.AnswerHorizontalPadding * (answers.Length - 1)) +
				(TextAdventure.Xna.Constants.MessageRenderer.AnswerHorizontalTextPadding * 2 * answers.Length) +
				answers.Sum(arg => arg.Size.X);
			float lineHeight = (_selectedAnswerWindowTexture.SpriteHeight * 2) + answers.Max(arg => arg.Size.Y);

			_lineSizesByLine[lineIndex] = new Vector2(lineWidth, lineHeight);
			_wordsByLine[lineIndex] = new List<MessageTextWord>(answers);
			_alignmentsByLine[lineIndex] = MessageTextAlignment.Center;
		}

		private MessageTextAnswer GetMessageTextAnswer(MessageQuestion question, MessageAnswer answer)
		{
			return new MessageTextAnswer(
				answer,
				answer.Text,
				_font.MeasureString(answer.Text) + TextAdventure.Xna.Constants.MessageRenderer.ShadowOffset,
				question.UnselectedAnswerForegroundColor,
				question.SelectedAnswerForegroundColor,
				question.SelectedAnswerBackgroundColor);
		}
	}
}