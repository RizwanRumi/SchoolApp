USE [master]
GO
/****** Object:  Database [StudentDB]    Script Date: 9/8/2017 10:17:00 AM ******/
CREATE DATABASE [StudentDB]
GO
USE [StudentDB]
GO
/****** Object:  Table [dbo].[tblCourse]    Script Date: 9/8/2017 10:17:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblCourse](
	[CourseId] [int] IDENTITY(1,1) NOT NULL,
	[CourseCode] [varchar](20) NOT NULL,
	[CourseName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_tblCourse] PRIMARY KEY CLUSTERED 
(
	[CourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblCourseComplete]    Script Date: 9/8/2017 10:17:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCourseComplete](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_tblCourseComplete] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblResult]    Script Date: 9/8/2017 10:17:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblResult](
	[ResId] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NOT NULL,
	[SemesterName] [varchar](50) NOT NULL,
	[Grade] [varchar](10) NOT NULL,
 CONSTRAINT [PK_tblResult] PRIMARY KEY CLUSTERED 
(
	[ResId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblStudent]    Script Date: 9/8/2017 10:17:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblStudent](
	[StudentId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Email] [varchar](200) NOT NULL,
	[Address] [varchar](200) NOT NULL,
	[Department] [varchar](200) NOT NULL,
	[Batch] [int] NOT NULL,
	[Enrolled] [datetime] NOT NULL,
	[Password] [varchar](100) NOT NULL,
 CONSTRAINT [PK_tblStudent] PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblTutorials]    Script Date: 9/8/2017 10:17:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblTutorials](
	[TutorialId] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NOT NULL,
	[FileName] [varchar](100) NOT NULL,
	[Complete] [bit] NOT NULL,
 CONSTRAINT [PK_tblTutorials] PRIMARY KEY CLUSTERED 
(
	[TutorialId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[tblCourse] ON 

INSERT [dbo].[tblCourse] ([CourseId], [CourseCode], [CourseName]) VALUES (1, N'ACT1021', N'Introduction to Accounting')
INSERT [dbo].[tblCourse] ([CourseId], [CourseCode], [CourseName]) VALUES (2, N'CSE1011', N'Programming Language I(C)')
INSERT [dbo].[tblCourse] ([CourseId], [CourseCode], [CourseName]) VALUES (3, N'CSE1012', N'Programming Language I(C) Lab')
INSERT [dbo].[tblCourse] ([CourseId], [CourseCode], [CourseName]) VALUES (4, N'MATH1024', N'Coordinate Geometry And Vector Analysis')
INSERT [dbo].[tblCourse] ([CourseId], [CourseCode], [CourseName]) VALUES (5, N'EEE1021', N'Electrical Circuits I')
INSERT [dbo].[tblCourse] ([CourseId], [CourseCode], [CourseName]) VALUES (6, N'EEE1022', N'Electrical Circuits I Lab')
INSERT [dbo].[tblCourse] ([CourseId], [CourseCode], [CourseName]) VALUES (7, N'MATH1034', N'Differential And Integral Calculus')
INSERT [dbo].[tblCourse] ([CourseId], [CourseCode], [CourseName]) VALUES (8, N'PHY1021', N'Physics I')
INSERT [dbo].[tblCourse] ([CourseId], [CourseCode], [CourseName]) VALUES (9, N'CSE1021', N'Descrete Mathematics')
INSERT [dbo].[tblCourse] ([CourseId], [CourseCode], [CourseName]) VALUES (10, N'ENG1011', N'English Fundamental Skills')
INSERT [dbo].[tblCourse] ([CourseId], [CourseCode], [CourseName]) VALUES (11, N'ENG1021', N'English For Engineers')
INSERT [dbo].[tblCourse] ([CourseId], [CourseCode], [CourseName]) VALUES (12, N'Math1035 ', N'Ordinary Differential Equation And Partial Differential Equation')
INSERT [dbo].[tblCourse] ([CourseId], [CourseCode], [CourseName]) VALUES (13, N'PHY1031', N'Physics II')
INSERT [dbo].[tblCourse] ([CourseId], [CourseCode], [CourseName]) VALUES (14, N'PHY1034', N'Physics Lab')
INSERT [dbo].[tblCourse] ([CourseId], [CourseCode], [CourseName]) VALUES (15, N'CSE1035', N'Algorithm')
SET IDENTITY_INSERT [dbo].[tblCourse] OFF
SET IDENTITY_INSERT [dbo].[tblCourseComplete] ON 

INSERT [dbo].[tblCourseComplete] ([Id], [StudentId], [CourseId], [Status]) VALUES (1, 1, 2, 1)
INSERT [dbo].[tblCourseComplete] ([Id], [StudentId], [CourseId], [Status]) VALUES (2, 1, 3, 1)
INSERT [dbo].[tblCourseComplete] ([Id], [StudentId], [CourseId], [Status]) VALUES (3, 1, 5, 1)
INSERT [dbo].[tblCourseComplete] ([Id], [StudentId], [CourseId], [Status]) VALUES (4, 1, 6, 1)
INSERT [dbo].[tblCourseComplete] ([Id], [StudentId], [CourseId], [Status]) VALUES (5, 1, 4, 0)
SET IDENTITY_INSERT [dbo].[tblCourseComplete] OFF
SET IDENTITY_INSERT [dbo].[tblResult] ON 

INSERT [dbo].[tblResult] ([ResId], [StudentId], [SemesterName], [Grade]) VALUES (1, 1, N'Spring 2010', N'A+')
INSERT [dbo].[tblResult] ([ResId], [StudentId], [SemesterName], [Grade]) VALUES (2, 1, N'Summer 2010', N'A-')
SET IDENTITY_INSERT [dbo].[tblResult] OFF
SET IDENTITY_INSERT [dbo].[tblStudent] ON 

INSERT [dbo].[tblStudent] ([StudentId], [Name], [Email], [Address], [Department], [Batch], [Enrolled], [Password]) VALUES (1, N'rizwan', N'rizwan@gmail.com', N'Dhaka', N'CSE', 24, CAST(0x0000A61B00000000 AS DateTime), N'123456')
SET IDENTITY_INSERT [dbo].[tblStudent] OFF
SET IDENTITY_INSERT [dbo].[tblTutorials] ON 

INSERT [dbo].[tblTutorials] ([TutorialId], [StudentId], [FileName], [Complete]) VALUES (1, 1, N'instruction_1383450643.docx', 1)
INSERT [dbo].[tblTutorials] ([TutorialId], [StudentId], [FileName], [Complete]) VALUES (2, 1, N'big_buck_bunny_240p_30mb_838170829.mp4', 0)
SET IDENTITY_INSERT [dbo].[tblTutorials] OFF
USE [master]
GO
ALTER DATABASE [StudentDB] SET  READ_WRITE 
GO
