USE [master]
GO
/****** Object:  Database [QuizDB]    Script Date: 27/06/2025 10:09:50 pm ******/
CREATE DATABASE [QuizDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QuizDB', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\QuizDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'QuizDB_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\QuizDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [QuizDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QuizDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QuizDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QuizDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QuizDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QuizDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QuizDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [QuizDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [QuizDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QuizDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QuizDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QuizDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QuizDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QuizDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QuizDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QuizDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QuizDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [QuizDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QuizDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QuizDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QuizDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QuizDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QuizDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QuizDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QuizDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [QuizDB] SET  MULTI_USER 
GO
ALTER DATABASE [QuizDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QuizDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QuizDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QuizDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [QuizDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [QuizDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [QuizDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [QuizDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [QuizDB]
GO
/****** Object:  Table [dbo].[User]    Script Date: 27/06/2025 10:09:51 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NULL,
	[Password] [varchar](255) NULL,
	[Gmail] [varchar](255) NULL,
	[DisplayName] [nvarchar](50) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[DeletedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Test]    Script Date: 27/06/2025 10:09:51 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Test](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[TopicId] [int] NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Participant]    Script Date: 27/06/2025 10:09:51 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Participant](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TestId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Score] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Result]    Script Date: 27/06/2025 10:09:51 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Result](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TestId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[QuizId] [int] NOT NULL,
	[IsTrue] [bit] NULL,
	[AttemptTime] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vw_UserTestResults]    Script Date: 27/06/2025 10:09:51 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--xem kết quả thi
CREATE VIEW [dbo].[vw_UserTestResults] AS
SELECT 
    u.DisplayName,
    t.Id AS TestId,
    t.TopicId,
    p.Score,
    COUNT(r.Id) AS TotalQuestions,
    SUM(CASE WHEN r.IsTrue = 1 THEN 1 ELSE 0 END) AS CorrectAnswers
FROM [User] u
JOIN Participant p ON u.Id = p.UserId
JOIN Test t ON t.Id = p.TestId
JOIN Result r ON r.TestId = t.Id AND r.UserId = u.Id
GROUP BY u.DisplayName, t.Id, t.TopicId, p.Score;

GO
/****** Object:  Table [dbo].[Category]    Script Date: 27/06/2025 10:09:51 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Quiz]    Script Date: 27/06/2025 10:09:51 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Quiz](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TopicId] [int] NOT NULL,
	[Question] [nvarchar](255) NOT NULL,
	[Answer] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Topic]    Script Date: 27/06/2025 10:09:51 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Topic](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[CreatedAt] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([Id], [Name]) VALUES (1, N'Định nghĩa')
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Quiz] ON 

INSERT [dbo].[Quiz] ([Id], [TopicId], [Question], [Answer]) VALUES (10, 3, N'tiếng việt', N'ngôn ngữ của nước Việt Nam')
INSERT [dbo].[Quiz] ([Id], [TopicId], [Question], [Answer]) VALUES (11, 3, N'tiếng anh', N'ngôn ngữ của nước Anh, Mỹ,..')
INSERT [dbo].[Quiz] ([Id], [TopicId], [Question], [Answer]) VALUES (12, 4, N'日本', N'nước nhật')
INSERT [dbo].[Quiz] ([Id], [TopicId], [Question], [Answer]) VALUES (13, 5, N'みます　II　見ます, 診ます', N'kiểm tra khám bệnh')
INSERT [dbo].[Quiz] ([Id], [TopicId], [Question], [Answer]) VALUES (14, 5, N'さがします　I　探します, 捜します', N'tìm, tìm kiếm')
INSERT [dbo].[Quiz] ([Id], [TopicId], [Question], [Answer]) VALUES (15, 5, N'おくねます II　遅れます', N'chậm, muộn (giờ)')
INSERT [dbo].[Quiz] ([Id], [TopicId], [Question], [Answer]) VALUES (16, 5, N'まにあいます  I  間に合います', N'kịp (giờ)')
INSERT [dbo].[Quiz] ([Id], [TopicId], [Question], [Answer]) VALUES (17, 5, N'やります I', N'làm')
INSERT [dbo].[Quiz] ([Id], [TopicId], [Question], [Answer]) VALUES (18, 5, N'ひろいます  I  拾います', N'nhặt, lượm')
INSERT [dbo].[Quiz] ([Id], [TopicId], [Question], [Answer]) VALUES (19, 5, N'れんらくします  III  連絡します', N'liên lạc')
INSERT [dbo].[Quiz] ([Id], [TopicId], [Question], [Answer]) VALUES (20, 5, N'きぶんがいい   気分がいい', N'cảm thấy thoải mái, cảm thấy khỏe')
INSERT [dbo].[Quiz] ([Id], [TopicId], [Question], [Answer]) VALUES (21, 5, N'きぶんがわるい　気分が悪い', N'cảm thấy không thoải mái, cảm thấy mệt')
INSERT [dbo].[Quiz] ([Id], [TopicId], [Question], [Answer]) VALUES (22, 5, N'うんどうかい　運動会', N'hội thi thể thao')
INSERT [dbo].[Quiz] ([Id], [TopicId], [Question], [Answer]) VALUES (23, 5, N'ぼんおどり　盆踊り', N'hội thi thể thao')
INSERT [dbo].[Quiz] ([Id], [TopicId], [Question], [Answer]) VALUES (24, 5, N'フリーマーケット', N'chợ đồ cũ, chợ trời')
INSERT [dbo].[Quiz] ([Id], [TopicId], [Question], [Answer]) VALUES (25, 5, N'ばしょ　場所', N'địa điểm')
INSERT [dbo].[Quiz] ([Id], [TopicId], [Question], [Answer]) VALUES (26, 5, N'ボランテイア', N'tình nguyện viên')
INSERT [dbo].[Quiz] ([Id], [TopicId], [Question], [Answer]) VALUES (27, 5, N'さいふ', N'ví')
INSERT [dbo].[Quiz] ([Id], [TopicId], [Question], [Answer]) VALUES (28, 5, N'ごみ', N'rác')
INSERT [dbo].[Quiz] ([Id], [TopicId], [Question], [Answer]) VALUES (29, 5, N'こっかいぎじどう　国会議事堂', N'tòa nhà quốc hội')
INSERT [dbo].[Quiz] ([Id], [TopicId], [Question], [Answer]) VALUES (30, 5, N'へいじつ　平日', N'ngày thường')
INSERT [dbo].[Quiz] ([Id], [TopicId], [Question], [Answer]) VALUES (31, 5, N'～べん　～弁', N'phương ngữ ~, tiếng ~, giọng ~')
INSERT [dbo].[Quiz] ([Id], [TopicId], [Question], [Answer]) VALUES (32, 5, N'こんど　今度', N'lần tới')
INSERT [dbo].[Quiz] ([Id], [TopicId], [Question], [Answer]) VALUES (33, 5, N'ずいぶん', N'khá, tương đối')
INSERT [dbo].[Quiz] ([Id], [TopicId], [Question], [Answer]) VALUES (34, 5, N'ちょくせつ　直接', N'trực tiếp')
INSERT [dbo].[Quiz] ([Id], [TopicId], [Question], [Answer]) VALUES (36, 2, N'string', N'string')
INSERT [dbo].[Quiz] ([Id], [TopicId], [Question], [Answer]) VALUES (37, 2, N'string 2', N'string 2')
INSERT [dbo].[Quiz] ([Id], [TopicId], [Question], [Answer]) VALUES (38, 6, N'to', N'de')
INSERT [dbo].[Quiz] ([Id], [TopicId], [Question], [Answer]) VALUES (39, 6, N'delete', N'xoa')
INSERT [dbo].[Quiz] ([Id], [TopicId], [Question], [Answer]) VALUES (40, 1, N'Khái niệm "Biến" trong lập trình là gì?', N'Một vị trí lưu trữ dữ liệu có tên.')
INSERT [dbo].[Quiz] ([Id], [TopicId], [Question], [Answer]) VALUES (41, 1, N'Thuật toán là gì?', N'Một dãy các bước cụ thể để giải quyết một vấn đề.')
INSERT [dbo].[Quiz] ([Id], [TopicId], [Question], [Answer]) VALUES (42, 1, N'Hàm là gì trong lập trình?', N'Một khối mã thực hiện một nhiệm vụ cụ thể.')
INSERT [dbo].[Quiz] ([Id], [TopicId], [Question], [Answer]) VALUES (43, 1, N'Biến toàn cục là gì?', N'Một biến được khai báo bên ngoài tất cả các hàm và có thể truy cập từ bất kỳ đâu trong chương trình.')
INSERT [dbo].[Quiz] ([Id], [TopicId], [Question], [Answer]) VALUES (44, 1, N'IDE là gì?', N'Môi trường phát triển tích hợp, hỗ trợ viết và gỡ lỗi mã nguồn.')
SET IDENTITY_INSERT [dbo].[Quiz] OFF
GO
SET IDENTITY_INSERT [dbo].[Topic] ON 

INSERT [dbo].[Topic] ([Id], [UserId], [CategoryId], [Title], [Description], [CreatedAt]) VALUES (1, 1, 1, N'Tổng hợp định nghĩa cơ bản', N'Các câu hỏi trắc nghiệm về định nghĩa các khái niệm cơ bản.', CAST(N'2025-06-07T13:58:59.723' AS DateTime))
INSERT [dbo].[Topic] ([Id], [UserId], [CategoryId], [Title], [Description], [CreatedAt]) VALUES (2, 2, 1, N'test create topic', N'string', CAST(N'2025-06-24T13:19:04.887' AS DateTime))
INSERT [dbo].[Topic] ([Id], [UserId], [CategoryId], [Title], [Description], [CreatedAt]) VALUES (3, 2, 1, N'test create topic ở web', N'kiểm tra tiếng việt', CAST(N'2025-06-24T20:56:06.720' AS DateTime))
INSERT [dbo].[Topic] ([Id], [UserId], [CategoryId], [Title], [Description], [CreatedAt]) VALUES (4, 2, 1, N'test tiếng nhật', N'test nhập quiz với tiếng nhật', CAST(N'2025-06-25T20:29:16.143' AS DateTime))
INSERT [dbo].[Topic] ([Id], [UserId], [CategoryId], [Title], [Description], [CreatedAt]) VALUES (5, 2, 1, N'Minano nihongo - Bài 26', N'', CAST(N'2025-06-27T10:21:24.297' AS DateTime))
INSERT [dbo].[Topic] ([Id], [UserId], [CategoryId], [Title], [Description], [CreatedAt]) VALUES (6, 2, 1, N'to-delete', N'', CAST(N'2025-06-27T16:14:18.207' AS DateTime))
SET IDENTITY_INSERT [dbo].[Topic] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [Username], [Password], [Gmail], [DisplayName], [CreatedAt], [DeletedAt]) VALUES (1, N'testuser', N'123456789', N'test@gmail.com', N'Test User', CAST(N'2025-06-07T13:58:59.720' AS DateTime), NULL)
INSERT [dbo].[User] ([Id], [Username], [Password], [Gmail], [DisplayName], [CreatedAt], [DeletedAt]) VALUES (2, N'string', N'$2a$11$AzuVXTAW782.T6JS/4bHS.O62PRTwiOwwcygww91XYZqlfIyMTaXa', NULL, N'string', CAST(N'2025-06-21T14:58:59.613' AS DateTime), NULL)
INSERT [dbo].[User] ([Id], [Username], [Password], [Gmail], [DisplayName], [CreatedAt], [DeletedAt]) VALUES (3, N'testRegister', N'$2a$11$Z/Eq6wprrG0QiRXazXpBz.QTiDI6vcZ3RWavCI1xxDkyXf.UDc27u', NULL, N'testRegister', CAST(N'2025-06-21T15:01:58.670' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
/****** Object:  Index [UQ_Result_UniqueAttempt]    Script Date: 27/06/2025 10:09:51 pm ******/
ALTER TABLE [dbo].[Result] ADD  CONSTRAINT [UQ_Result_UniqueAttempt] UNIQUE NONCLUSTERED 
(
	[TestId] ASC,
	[UserId] ASC,
	[QuizId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Result] ADD  DEFAULT (getdate()) FOR [AttemptTime]
GO
ALTER TABLE [dbo].[Topic] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Participant]  WITH CHECK ADD FOREIGN KEY([TestId])
REFERENCES [dbo].[Test] ([Id])
GO
ALTER TABLE [dbo].[Participant]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Quiz]  WITH CHECK ADD FOREIGN KEY([TopicId])
REFERENCES [dbo].[Topic] ([Id])
GO
ALTER TABLE [dbo].[Result]  WITH CHECK ADD FOREIGN KEY([QuizId])
REFERENCES [dbo].[Quiz] ([Id])
GO
ALTER TABLE [dbo].[Result]  WITH CHECK ADD FOREIGN KEY([TestId])
REFERENCES [dbo].[Test] ([Id])
GO
ALTER TABLE [dbo].[Result]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Test]  WITH CHECK ADD FOREIGN KEY([TopicId])
REFERENCES [dbo].[Topic] ([Id])
GO
ALTER TABLE [dbo].[Test]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Topic]  WITH CHECK ADD FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
GO
ALTER TABLE [dbo].[Topic]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
/****** Object:  StoredProcedure [dbo].[CalculateTestScore]    Script Date: 27/06/2025 10:09:51 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--tính toán điểm
CREATE PROCEDURE [dbo].[CalculateTestScore]
    @TestId INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE P
    SET P.Score = Sub.Score
    FROM Participant P
    JOIN (
        SELECT UserId, COUNT(*) * 1.0 AS Score
        FROM Result
        WHERE TestId = @TestId AND IsTrue = 1
        GROUP BY UserId
    ) Sub ON P.UserId = Sub.UserId AND P.TestId = @TestId;
END;

GO
/****** Object:  StoredProcedure [dbo].[CreateTestAndAddParticipants]    Script Date: 27/06/2025 10:09:51 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--thêm danh sách người tham gia
CREATE PROCEDURE [dbo].[CreateTestAndAddParticipants]
    @UserId INT,
    @TopicId INT,
    @StartTime DATETIME,
    @EndTime DATETIME,
    @ParticipantIds NVARCHAR(MAX) -- Chuỗi ID cách nhau bởi dấu phẩy: '2,3,4'
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Test (UserId, TopicId, StartTime, EndTime)
    VALUES (@UserId, @TopicId, @StartTime, @EndTime);

    DECLARE @TestId INT = SCOPE_IDENTITY();

    -- Convert comma-separated IDs to table
    DECLARE @xml XML = '<i>' + REPLACE(@ParticipantIds, ',', '</i><i>') + '</i>';

    INSERT INTO Participant (TestId, UserId)
    SELECT @TestId, T.N.value('.', 'INT')
    FROM @xml.nodes('/i') AS T(N);
END;

GO
/****** Object:  StoredProcedure [dbo].[ResetUserResults]    Script Date: 27/06/2025 10:09:51 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--reset kết quả thi
CREATE PROCEDURE [dbo].[ResetUserResults]
    @TestId INT,
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Result
    SET 
        IsTrue = NULL,
        AttemptTime = GETDATE()
    WHERE 
        TestId = @TestId
        AND UserId = @UserId;
END;
GO
USE [master]
GO
ALTER DATABASE [QuizDB] SET  READ_WRITE 
GO
