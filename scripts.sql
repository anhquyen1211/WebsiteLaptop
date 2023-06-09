USE [LQShop]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 5/9/2023 12:16:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[Account_id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Password] [varchar](max) NULL,
	[Request_code] [nvarchar](max) NULL,
	[Role] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](10) NOT NULL,
	[Avatar] [text] NULL,
	[Create_by] [nvarchar](100) NULL,
	[Create_at] [datetime] NOT NULL,
	[Update_by] [nvarchar](100) NULL,
	[Update_at] [datetime] NOT NULL,
	[Status] [char](128) NULL,
 CONSTRAINT [PK_dbo.Account] PRIMARY KEY CLUSTERED 
(
	[Account_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccountAddress]    Script Date: 5/9/2023 12:16:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountAddress](
	[Account_address_id] [int] IDENTITY(1,1) NOT NULL,
	[Account_id] [int] NOT NULL,
	[Province_id] [int] NOT NULL,
	[District_id] [int] NOT NULL,
	[Ward_id] [int] NOT NULL,
	[Account_phonenumber] [nvarchar](10) NULL,
	[Account_username] [nvarchar](20) NULL,
	[Content] [nvarchar](50) NULL,
	[IsDefault] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.AccountAddress] PRIMARY KEY CLUSTERED 
(
	[Account_address_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Brand]    Script Date: 5/9/2023 12:16:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Brand](
	[Brand_id] [int] IDENTITY(1,1) NOT NULL,
	[Brand_name] [nvarchar](50) NOT NULL,
	[Create_by] [varchar](100) NOT NULL,
	[Create_at] [datetime] NOT NULL,
	[Update_by] [nvarchar](100) NOT NULL,
	[Update_at] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Brand] PRIMARY KEY CLUSTERED 
(
	[Brand_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Delivery]    Script Date: 5/9/2023 12:16:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Delivery](
	[Delivery_id] [int] IDENTITY(1,1) NOT NULL,
	[Delivery_name] [nvarchar](100) NOT NULL,
	[Price] [money] NOT NULL,
	[Create_at] [datetime] NOT NULL,
	[Create_by] [varchar](20) NOT NULL,
	[Status] [char](1) NULL,
	[Update_by] [nvarchar](20) NOT NULL,
	[Update_at] [datetime] NULL,
 CONSTRAINT [PK_dbo.Delivery] PRIMARY KEY CLUSTERED 
(
	[Delivery_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Discount]    Script Date: 5/9/2023 12:16:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Discount](
	[Disscount_id] [int] IDENTITY(1,1) NOT NULL,
	[Discount_name] [nvarchar](100) NOT NULL,
	[Discount_star] [datetime] NOT NULL,
	[Discount_end] [datetime] NOT NULL,
	[Discount_price] [float] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Discount_code] [nvarchar](10) NULL,
	[Create_at] [datetime] NOT NULL,
	[Create_by] [nvarchar](100) NOT NULL,
	[Update_by] [nvarchar](100) NOT NULL,
	[Update_at] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Discount] PRIMARY KEY CLUSTERED 
(
	[Disscount_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Districts]    Script Date: 5/9/2023 12:16:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Districts](
	[District_id] [int] IDENTITY(1,1) NOT NULL,
	[Province_id] [int] NOT NULL,
	[District_name] [nvarchar](50) NOT NULL,
	[Type] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_dbo.Districts] PRIMARY KEY CLUSTERED 
(
	[District_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Feedback]    Script Date: 5/9/2023 12:16:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedback](
	[Feedback_id] [int] IDENTITY(1,1) NOT NULL,
	[Account_id] [int] NOT NULL,
	[Product_id] [int] NOT NULL,
	[Genre_id] [int] NOT NULL,
	[Disscount_id] [int] NOT NULL,
	[Content] [nvarchar](max) NULL,
	[Rate_star] [int] NOT NULL,
	[Create_at] [datetime] NOT NULL,
	[Create_by] [varchar](100) NOT NULL,
	[Status] [char](1) NULL,
	[Update_by] [nvarchar](100) NOT NULL,
	[Update_at] [datetime] NULL,
 CONSTRAINT [PK_dbo.Feedback] PRIMARY KEY CLUSTERED 
(
	[Feedback_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genre]    Script Date: 5/9/2023 12:16:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genre](
	[Genre_id] [int] IDENTITY(1,1) NOT NULL,
	[Genre_name] [nvarchar](50) NOT NULL,
	[Create_at] [datetime] NOT NULL,
	[Create_by] [varchar](100) NOT NULL,
	[Update_by] [nvarchar](100) NULL,
	[Update_at] [datetime] NULL,
 CONSTRAINT [PK_dbo.Genre] PRIMARY KEY CLUSTERED 
(
	[Genre_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 5/9/2023 12:16:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[Order_id] [int] IDENTITY(1,1) NOT NULL,
	[Account_id] [int] NOT NULL,
	[OrderAddressId] [int] NULL,
	[Payment_id] [int] NOT NULL,
	[Delivery_id] [int] NOT NULL,
	[Order_date] [datetime] NOT NULL,
	[Status] [char](1) NULL,
	[Order_note] [nvarchar](200) NULL,
	[Create_at] [datetime] NOT NULL,
	[Total] [float] NOT NULL,
	[Create_by] [varchar](100) NOT NULL,
	[Update_by] [nvarchar](100) NOT NULL,
	[Update_at] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Order] PRIMARY KEY CLUSTERED 
(
	[Order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderAddress]    Script Date: 5/9/2023 12:16:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderAddress](
	[OrderAddressId] [int] IDENTITY(1,1) NOT NULL,
	[Province_id] [int] NULL,
	[District_id] [int] NULL,
	[Ward_id] [int] NULL,
	[OrderPhonenumber] [nvarchar](10) NULL,
	[OrderUsername] [nvarchar](20) NULL,
	[Content] [nvarchar](150) NULL,
 CONSTRAINT [PK_dbo.OrderAddress] PRIMARY KEY CLUSTERED 
(
	[OrderAddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 5/9/2023 12:16:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[Product_id] [int] NOT NULL,
	[Genre_id] [int] NOT NULL,
	[Disscount_id] [int] NOT NULL,
	[Order_id] [int] NOT NULL,
	[Price] [float] NOT NULL,
	[Status] [char](1) NULL,
	[Quantity] [int] NOT NULL,
	[Create_by] [varchar](20) NOT NULL,
	[Create_at] [datetime] NOT NULL,
	[Update_by] [nvarchar](20) NOT NULL,
	[Update_at] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.OrderDetail] PRIMARY KEY CLUSTERED 
(
	[Product_id] ASC,
	[Genre_id] ASC,
	[Disscount_id] ASC,
	[Order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 5/9/2023 12:16:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[Payment_id] [int] IDENTITY(1,1) NOT NULL,
	[Payment_name] [nvarchar](50) NOT NULL,
	[Create_at] [datetime] NOT NULL,
	[Create_by] [varchar](20) NOT NULL,
	[Status] [char](1) NULL,
	[Update_by] [nvarchar](20) NULL,
	[Update_at] [datetime] NULL,
 CONSTRAINT [PK_dbo.Payment] PRIMARY KEY CLUSTERED 
(
	[Payment_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 5/9/2023 12:16:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Product_id] [int] IDENTITY(1,1) NOT NULL,
	[Genre_id] [int] NOT NULL,
	[Disscount_id] [int] NOT NULL,
	[Brand_id] [int] NOT NULL,
	[Product_name] [nvarchar](200) NOT NULL,
	[Price] [float] NOT NULL,
	[View] [bigint] NOT NULL,
	[Buyturn] [bigint] NOT NULL,
	[Quantity] [char](10) NOT NULL,
	[Status] [char](1) NULL,
	[Create_by] [varchar](100) NOT NULL,
	[Create_at] [datetime] NOT NULL,
	[Update_by] [nvarchar](100) NULL,
	[Update_at] [datetime] NOT NULL,
	[Type] [int] NOT NULL,
	[Specifications] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Product] PRIMARY KEY CLUSTERED 
(
	[Product_id] ASC,
	[Genre_id] ASC,
	[Disscount_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductImages]    Script Date: 5/9/2023 12:16:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductImages](
	[Product_img_id] [int] IDENTITY(1,1) NOT NULL,
	[Product_id] [int] NOT NULL,
	[Genre_id] [int] NOT NULL,
	[Disscount_id] [int] NOT NULL,
	[Image] [nvarchar](500) NULL,
 CONSTRAINT [PK_dbo.ProductImages] PRIMARY KEY CLUSTERED 
(
	[Product_img_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Provinces]    Script Date: 5/9/2023 12:16:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Provinces](
	[Province_id] [int] IDENTITY(1,1) NOT NULL,
	[Province_name] [nvarchar](50) NOT NULL,
	[Type] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_dbo.Provinces] PRIMARY KEY CLUSTERED 
(
	[Province_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReplyFeedback]    Script Date: 5/9/2023 12:16:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReplyFeedback](
	[Rep_feedback_id] [int] IDENTITY(1,1) NOT NULL,
	[Feedback_id] [int] NOT NULL,
	[Account_id] [int] NOT NULL,
	[Content] [nvarchar](max) NULL,
	[Status] [nvarchar](1) NULL,
	[Create_at] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.ReplyFeedback] PRIMARY KEY CLUSTERED 
(
	[Rep_feedback_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Wards]    Script Date: 5/9/2023 12:16:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Wards](
	[Ward_id] [int] IDENTITY(1,1) NOT NULL,
	[District_id] [int] NOT NULL,
	[Ward_name] [nvarchar](50) NOT NULL,
	[Type] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_dbo.Wards] PRIMARY KEY CLUSTERED 
(
	[Ward_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AccountAddress]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AccountAddress_dbo.Account_Account_id] FOREIGN KEY([Account_id])
REFERENCES [dbo].[Account] ([Account_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AccountAddress] CHECK CONSTRAINT [FK_dbo.AccountAddress_dbo.Account_Account_id]
GO
ALTER TABLE [dbo].[AccountAddress]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AccountAddress_dbo.Districts_District_id] FOREIGN KEY([District_id])
REFERENCES [dbo].[Districts] ([District_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AccountAddress] CHECK CONSTRAINT [FK_dbo.AccountAddress_dbo.Districts_District_id]
GO
ALTER TABLE [dbo].[AccountAddress]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AccountAddress_dbo.Provinces_Province_id] FOREIGN KEY([Province_id])
REFERENCES [dbo].[Provinces] ([Province_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AccountAddress] CHECK CONSTRAINT [FK_dbo.AccountAddress_dbo.Provinces_Province_id]
GO
ALTER TABLE [dbo].[AccountAddress]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AccountAddress_dbo.Wards_Ward_id] FOREIGN KEY([Ward_id])
REFERENCES [dbo].[Wards] ([Ward_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AccountAddress] CHECK CONSTRAINT [FK_dbo.AccountAddress_dbo.Wards_Ward_id]
GO
ALTER TABLE [dbo].[Districts]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Districts_dbo.Provinces_Province_id] FOREIGN KEY([Province_id])
REFERENCES [dbo].[Provinces] ([Province_id])
GO
ALTER TABLE [dbo].[Districts] CHECK CONSTRAINT [FK_dbo.Districts_dbo.Provinces_Province_id]
GO
ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Feedback_dbo.Account_Account_id] FOREIGN KEY([Account_id])
REFERENCES [dbo].[Account] ([Account_id])
GO
ALTER TABLE [dbo].[Feedback] CHECK CONSTRAINT [FK_dbo.Feedback_dbo.Account_Account_id]
GO
ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Feedback_dbo.Product_Product_id_Genre_id_Disscount_id] FOREIGN KEY([Product_id], [Genre_id], [Disscount_id])
REFERENCES [dbo].[Product] ([Product_id], [Genre_id], [Disscount_id])
GO
ALTER TABLE [dbo].[Feedback] CHECK CONSTRAINT [FK_dbo.Feedback_dbo.Product_Product_id_Genre_id_Disscount_id]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Order_dbo.Account_Account_id] FOREIGN KEY([Account_id])
REFERENCES [dbo].[Account] ([Account_id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_dbo.Order_dbo.Account_Account_id]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Order_dbo.Delivery_Delivery_id] FOREIGN KEY([Delivery_id])
REFERENCES [dbo].[Delivery] ([Delivery_id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_dbo.Order_dbo.Delivery_Delivery_id]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Order_dbo.OrderAddress_OrderAddressId] FOREIGN KEY([OrderAddressId])
REFERENCES [dbo].[OrderAddress] ([OrderAddressId])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_dbo.Order_dbo.OrderAddress_OrderAddressId]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Order_dbo.Payment_Payment_id] FOREIGN KEY([Payment_id])
REFERENCES [dbo].[Payment] ([Payment_id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_dbo.Order_dbo.Payment_Payment_id]
GO
ALTER TABLE [dbo].[OrderAddress]  WITH CHECK ADD  CONSTRAINT [FK_dbo.OrderAddress_dbo.Districts_District_id] FOREIGN KEY([District_id])
REFERENCES [dbo].[Districts] ([District_id])
GO
ALTER TABLE [dbo].[OrderAddress] CHECK CONSTRAINT [FK_dbo.OrderAddress_dbo.Districts_District_id]
GO
ALTER TABLE [dbo].[OrderAddress]  WITH CHECK ADD  CONSTRAINT [FK_dbo.OrderAddress_dbo.Provinces_Province_id] FOREIGN KEY([Province_id])
REFERENCES [dbo].[Provinces] ([Province_id])
GO
ALTER TABLE [dbo].[OrderAddress] CHECK CONSTRAINT [FK_dbo.OrderAddress_dbo.Provinces_Province_id]
GO
ALTER TABLE [dbo].[OrderAddress]  WITH CHECK ADD  CONSTRAINT [FK_dbo.OrderAddress_dbo.Wards_Ward_id] FOREIGN KEY([Ward_id])
REFERENCES [dbo].[Wards] ([Ward_id])
GO
ALTER TABLE [dbo].[OrderAddress] CHECK CONSTRAINT [FK_dbo.OrderAddress_dbo.Wards_Ward_id]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_dbo.OrderDetail_dbo.Order_Order_id] FOREIGN KEY([Order_id])
REFERENCES [dbo].[Order] ([Order_id])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_dbo.OrderDetail_dbo.Order_Order_id]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_dbo.OrderDetail_dbo.Product_Product_id_Genre_id_Disscount_id] FOREIGN KEY([Product_id], [Genre_id], [Disscount_id])
REFERENCES [dbo].[Product] ([Product_id], [Genre_id], [Disscount_id])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_dbo.OrderDetail_dbo.Product_Product_id_Genre_id_Disscount_id]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Product_dbo.Brand_Brand_id] FOREIGN KEY([Brand_id])
REFERENCES [dbo].[Brand] ([Brand_id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_dbo.Product_dbo.Brand_Brand_id]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Product_dbo.Discount_Disscount_id] FOREIGN KEY([Disscount_id])
REFERENCES [dbo].[Discount] ([Disscount_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_dbo.Product_dbo.Discount_Disscount_id]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Product_dbo.Genre_Genre_id] FOREIGN KEY([Genre_id])
REFERENCES [dbo].[Genre] ([Genre_id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_dbo.Product_dbo.Genre_Genre_id]
GO
ALTER TABLE [dbo].[ProductImages]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ProductImages_dbo.Product_Product_id_Genre_id_Disscount_id] FOREIGN KEY([Product_id], [Genre_id], [Disscount_id])
REFERENCES [dbo].[Product] ([Product_id], [Genre_id], [Disscount_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductImages] CHECK CONSTRAINT [FK_dbo.ProductImages_dbo.Product_Product_id_Genre_id_Disscount_id]
GO
ALTER TABLE [dbo].[ReplyFeedback]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ReplyFeedback_dbo.Account_Account_id] FOREIGN KEY([Account_id])
REFERENCES [dbo].[Account] ([Account_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ReplyFeedback] CHECK CONSTRAINT [FK_dbo.ReplyFeedback_dbo.Account_Account_id]
GO
ALTER TABLE [dbo].[ReplyFeedback]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ReplyFeedback_dbo.Feedback_Feedback_id] FOREIGN KEY([Feedback_id])
REFERENCES [dbo].[Feedback] ([Feedback_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ReplyFeedback] CHECK CONSTRAINT [FK_dbo.ReplyFeedback_dbo.Feedback_Feedback_id]
GO
ALTER TABLE [dbo].[Wards]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Wards_dbo.Districts_District_id] FOREIGN KEY([District_id])
REFERENCES [dbo].[Districts] ([District_id])
GO
ALTER TABLE [dbo].[Wards] CHECK CONSTRAINT [FK_dbo.Wards_dbo.Districts_District_id]
GO
