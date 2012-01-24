﻿namespace TextAdventure.Engine.Objects
{
	// For readability, do not reorder members with ReSharper
	public struct Symbol
	{
		public const byte Null = 0;
		public const byte OutlinedSmiley = 1;
		public const byte FilledSmiley = 2;
		public const byte Heart = 3;
		public const byte Diamond = 4;
		public const byte Club = 5;
		public const byte Spade = 6;
		public const byte Bullet = 7;
		public const byte InverseBullet = 8;
		public const byte Circle = 9;
		public const byte InverseCircle = 10;
		public const byte Male = 11;
		public const byte Female = 12;
		public const byte EighthNote = 13;
		public const byte BarredEighthNote = 14;
		public const byte SunWithRays = 15;
		public const byte RightTriangle = 16;
		public const byte LeftTriangle = 17;
		public const byte UpDownArrow = 18;
		public const byte DoubleExclamationMark = 19;
		public const byte Pilcrow = 20;
		public const byte Section = 21;
		public const byte Rectangle = 22;
		public const byte UpDownArrowWithBaseline = 23;
		public const byte UpArrow = 24;
		public const byte DownArrow = 25;
		public const byte RightArrow = 26;
		public const byte LeftArrow = 27;
		public const byte Angle = 28;
		public const byte LeftRightArrow = 29;
		public const byte UpTriangle = 30;
		public const byte DownTriangle = 31;
		public const byte Space = 32;
		public const byte ExclamationMark = 33;
		public const byte QuotationMark = 34;
		public const byte Number = 35;
		public const byte Dollar = 36;
		public const byte Percent = 37;
		public const byte Ampersand = 38;
		public const byte Apostrophe = 39;
		public const byte LeftParenthesis = 40;
		public const byte RightParenthesis = 41;
		public const byte Asterisk = 42;
		public const byte PlusSign = 43;
		public const byte Comma = 44;
		public const byte MinusSign = 45;
		public const byte Period = 46;
		public const byte ForwardSlash = 47;
		public const byte Zero = 48;
		public const byte One = 49;
		public const byte Two = 50;
		public const byte Three = 51;
		public const byte Four = 52;
		public const byte Five = 53;
		public const byte Six = 54;
		public const byte Seven = 55;
		public const byte Eight = 56;
		public const byte Nine = 57;
		public const byte Colon = 58;
		public const byte Semicolon = 59;
		public const byte LessThan = 60;
		public const byte Equal = 61;
		public const byte GreaterThan = 62;
		public const byte QuestionMark = 63;
		public const byte At = 64;
		public const byte UppercaseA = 65;
		public const byte UppercaseB = 66;
		public const byte UppercaseC = 67;
		public const byte UppercaseD = 68;
		public const byte UppercaseE = 69;
		public const byte UppercaseF = 70;
		public const byte UppercaseG = 71;
		public const byte UppercaseH = 72;
		public const byte UppercaseI = 73;
		public const byte UppercaseJ = 74;
		public const byte UppercaseK = 75;
		public const byte UppercaseL = 76;
		public const byte UppercaseM = 77;
		public const byte UppercaseN = 78;
		public const byte UppercaseO = 79;
		public const byte UppercaseP = 80;
		public const byte UppercaseQ = 81;
		public const byte UppercaseR = 82;
		public const byte UppercaseS = 83;
		public const byte UppercaseT = 84;
		public const byte UppercaseU = 85;
		public const byte UppercaseV = 86;
		public const byte UppercaseW = 87;
		public const byte UppercaseX = 88;
		public const byte UppercaseY = 89;
		public const byte UppercaseZ = 90;
		public const byte LeftBracket = 91;
		public const byte Backslash = 92;
		public const byte RightBracket = 93;
		public const byte Caret = 94;
		public const byte Underscore = 95;
		public const byte GraveAccent = 96;
		public const byte LowercaseA = 97;
		public const byte LowercaseB = 98;
		public const byte LowercaseC = 99;
		public const byte LowercaseD = 100;
		public const byte LowercaseE = 101;
		public const byte LowercaseF = 102;
		public const byte LowercaseG = 103;
		public const byte LowercaseH = 104;
		public const byte LowercaseI = 105;
		public const byte LowercaseJ = 106;
		public const byte LowercaseK = 107;
		public const byte LowercaseL = 108;
		public const byte LowercaseM = 109;
		public const byte LowercaseN = 110;
		public const byte LowercaseO = 111;
		public const byte LowercaseP = 112;
		public const byte LowercaseQ = 113;
		public const byte LowercaseR = 114;
		public const byte LowercaseS = 115;
		public const byte LowercaseT = 116;
		public const byte LowercaseU = 117;
		public const byte LowercaseV = 118;
		public const byte LowercaseW = 119;
		public const byte LowercaseX = 120;
		public const byte LowercaseY = 121;
		public const byte LowercaseZ = 122;
		public const byte LeftBrace = 123;
		public const byte VerticalBar = 124;
		public const byte RightBrace = 125;
		public const byte Tilde = 126;
		public const byte House = 127;
		public const byte CapitalCCedilla = 128;
		public const byte LowercaseUUmlaut = 129;
		public const byte LowercaseEAcute = 130;
		public const byte LowercaseACircumflex = 131;
		public const byte LowercaseAUmlaut = 132;
		public const byte LowercaseAGrave = 133;
		public const byte LowercaseARing = 134;
		public const byte LowercaseCCedilla = 135;
		public const byte LowercaseECircumflex = 136;
		public const byte LowercaseEUmlaut = 137;
		public const byte LowercaseEGrave = 138;
		public const byte LowercaseIUmlaut = 139;
		public const byte LowercaseICircumflex = 140;
		public const byte LowercaseIGrave = 141;
		public const byte UppercaseAUmlaut = 142;
		public const byte UppercaseARing = 143;
		public const byte UppercaseEAcute = 144;
		public const byte LowercaseAsh = 145;
		public const byte UppercaseAsh = 146;
		public const byte LowercaseOCircumflex = 147;
		public const byte LowercaseOUmlaut = 148;
		public const byte LowercaseOGrave = 149;
		public const byte LowercaseUCircumflex = 150;
		public const byte LowercaseUGrave = 151;
		public const byte LowercaseYUmlaut = 152;
		public const byte UppercaseOUmlaut = 153;
		public const byte UppercaseUUmlaut = 154;
		public const byte Cent = 155;
		public const byte Pound = 156;
		public const byte Yen = 157;
		public const byte Peseta = 158;
		public const byte Florin = 159;
		public const byte LowercaseAAcute = 160;
		public const byte LowercaseIAcute = 161;
		public const byte LowercaseOAcute = 162;
		public const byte LowercaseUAcute = 163;
		public const byte LowercaseNTilde = 164;
		public const byte UppercaseNTilde = 165;
		public const byte OrdinalIndicatorA = 166;
		public const byte OrdinalIndicatorO = 167;
		public const byte InvertedQuestionMark = 168;
		public const byte ReversedNegation = 169;
		public const byte Negation = 170;
		public const byte OneHalf = 171;
		public const byte OneQuarter = 172;
		public const byte InvertedExclamationMark = 173;
		public const byte LeftGuillemet = 174;
		public const byte RightGuillemet = 175;
		public const byte LightShade = 176;
		public const byte MediumShade = 177;
		public const byte DarkShade = 178;
		public const byte SingleVertical = 179;
		public const byte SingleVerticalAndSingleLeft = 180;
		public const byte SingleVerticalAndDoubleLeft = 181;
		public const byte DoubleVerticalAndSingleLeft = 182;
		public const byte DoubleDownAndSingleLeft = 183;
		public const byte SingleDownAndDoubleLeft = 184;
		public const byte DoubleVerticalAndDoubleLeft = 185;
		public const byte DoubleVertical = 186;
		public const byte DoubleDownAndDoubleLeft = 187;
		public const byte DoubleUpAndDoubleLeft = 188;
		public const byte DoubleUpAndSingleLeft = 189;
		public const byte SingleUpAndDoubleLeft = 190;
		public const byte SingleDownAndSingleLeft = 191;
		public const byte SingleUpAndSingleRight = 192;
		public const byte SingleUpAndSingleHorizontal = 193;
		public const byte SingleDownAndSingleHorizontal = 194;
		public const byte SingleVerticalAndSingleRight = 195;
		public const byte SingleHorizontal = 196;
		public const byte SingleVerticalAndSingleHorizontal = 197;
		public const byte SingleVerticalAndDoubleRight = 198;
		public const byte DoubleVerticalAndSingleRight = 199;
		public const byte DoubleUpAndDoubleRight = 200;
		public const byte DoubleDownAndDoubleRight = 201;
		public const byte DoubleUpAndDoubleHorizontal = 202;
		public const byte DoubleDownAndDoubleHorizontal = 203;
		public const byte DoubleVerticalAndDoubleRight = 204;
		public const byte DoubleHorizontal = 205;
		public const byte DoubleVerticalAndDoubleHorizontal = 206;
		public const byte SingleUpAndDoubleHorizontal = 207;
		public const byte DoubleUpAndSingleHorizontal = 208;
		public const byte SingleDownAndDoubleHorizontal = 209;
		public const byte DoubleDownAndSingleHorizontal = 210;
		public const byte DoubleUpAndSingleRight = 211;
		public const byte SingleUpAndDoubleRight = 212;
		public const byte SingleDownAndDoubleRight = 213;
		public const byte DoubleDownAndSingleRight = 214;
		public const byte DoubleVerticalAndSingleHorizontal = 215;
		public const byte SingleVerticalAndDoubleHorizontal = 216;
		public const byte SingleUpAndSingleLeft = 217;
		public const byte SingleDownAndSingleRight = 218;
		public const byte FullBlock = 219;
		public const byte LowerHalfBlock = 220;
		public const byte LeftHalfBlock = 221;
		public const byte RightHalfBlock = 222;
		public const byte UpperHalfBlock = 223;
		public const byte LowercaseAlpha = 224;
		public const byte LowercaseSharpS = 225;
		public const byte UppercaseGamma = 226;
		public const byte LowercasePi = 227;
		public const byte UppercaseSigma = 228;
		public const byte LowercaseSigma = 229;
		public const byte LowercaseMu = 230;
		public const byte LowercaseTau = 231;
		public const byte UppercasePhi = 232;
		public const byte UppercaseTheta = 233;
		public const byte UppercaseOmega = 234;
		public const byte LowercaseDelta = 235;
		public const byte Infinity = 236;
		public const byte LowercasePhi = 237;
		public const byte LowercaseEpsilon = 238;
		public const byte Intersection = 239;
		public const byte TripleBar = 240;
		public const byte PlusMinus = 241;
		public const byte GreaterThanOrEqual = 242;
		public const byte LessThanOrEqual = 243;
		public const byte IntegralTopHalf = 244;
		public const byte IntegralBottomHalf = 245;
		public const byte Obelus = 246;
		public const byte ApproximatelyEqual = 247;
		public const byte Degree = 248;
		public const byte BulletOperator = 249;
		public const byte MiddleDot = 250;
		public const byte SquareRoot = 251;
		public const byte NasalRelease = 252;
		public const byte SquarePower = 253;
		public const byte Square = 254;
		public const byte NonBreakingSpace = 255;
	}
}