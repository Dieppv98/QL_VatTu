USE [master]
GO
/****** Object:  Database [Materials]    Script Date: 26/01/2021 4:45:59 CH ******/
CREATE DATABASE [Materials] ON  PRIMARY 
( NAME = N'[Materials]', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\Materials.mdf' , SIZE = 10240KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'[Materials]_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\Materials_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Materials].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Materials] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Materials] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Materials] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Materials] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Materials] SET ARITHABORT OFF 
GO
ALTER DATABASE [Materials] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Materials] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Materials] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Materials] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Materials] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Materials] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Materials] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Materials] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Materials] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Materials] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Materials] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Materials] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Materials] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Materials] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Materials] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Materials] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Materials] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Materials] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Materials] SET  MULTI_USER 
GO
ALTER DATABASE [Materials] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Materials] SET DB_CHAINING OFF 
GO
USE [Materials]
GO
/****** Object:  User [hoangdd]    Script Date: 26/01/2021 4:45:59 CH ******/
CREATE USER [hoangdd] FOR LOGIN [hoangdd] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [diep]    Script Date: 26/01/2021 4:45:59 CH ******/
CREATE USER [diep] FOR LOGIN [diep] WITH DEFAULT_SCHEMA=[dbo]
GO
sys.sp_addrolemember @rolename = N'db_owner', @membername = N'hoangdd'
GO
sys.sp_addrolemember @rolename = N'db_datareader', @membername = N'hoangdd'
GO
sys.sp_addrolemember @rolename = N'db_datawriter', @membername = N'hoangdd'
GO
sys.sp_addrolemember @rolename = N'db_owner', @membername = N'diep'
GO
sys.sp_addrolemember @rolename = N'db_datareader', @membername = N'diep'
GO
sys.sp_addrolemember @rolename = N'db_datawriter', @membername = N'diep'
GO
/****** Object:  Table [dbo].[Action]    Script Date: 26/01/2021 4:45:59 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Action](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ActionName] [nchar](100) NULL,
	[Value] [int] NULL,
	[Description] [nchar](1000) NULL,
 CONSTRAINT [PK_Action] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Admin]    Script Date: 26/01/2021 4:45:59 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admin](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[AdminCode] [nvarchar](50) NULL,
	[AdminName] [nvarchar](200) NULL,
	[UserName] [nvarchar](50) NULL,
	[PassWord] [nvarchar](50) NULL,
 CONSTRAINT [PK_Admin] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ChiTietCheBan]    Script Date: 26/01/2021 4:45:59 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietCheBan](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ten_tay_in] [nvarchar](500) NULL,
	[so_kem] [int] NULL,
	[so_bo] [int] NULL,
	[tong] [int] NULL,
	[trang] [int] NULL,
	[kho_in] [nchar](20) NULL,
	[kho_kem] [nchar](20) NULL,
	[phuong_phap_in] [nchar](20) NULL,
	[don_hang_id] [int] NULL,
 CONSTRAINT [PK_ChiTietCheBan] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ChiTietDuToan]    Script Date: 26/01/2021 4:45:59 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietDuToan](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[created_date] [datetime] NULL,
	[last_updated] [datetime] NULL,
	[ten] [nvarchar](500) NULL,
	[dinh_luong] [int] NULL,
	[dinh_luong_thuc_te] [int] NULL,
	[kho_giay] [int] NULL,
	[ma_vat_tu] [nchar](20) NULL,
	[trang] [int] NULL,
	[dinh_muc] [int] NULL,
	[sl_chinh] [int] NULL,
	[sl_bu_in] [int] NULL,
	[sl_bu_tp] [int] NULL,
	[sl_tong] [int] NULL,
	[don_vi_tinh] [nvarchar](50) NULL,
	[muc_dich_sd] [nvarchar](500) NULL,
	[cc_vat_tu] [nvarchar](50) NULL,
	[thoi_han_cc] [datetime] NULL,
	[don_hang_id] [int] NOT NULL,
 CONSTRAINT [PK_ChiTietDuToan] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ChiTietIn]    Script Date: 26/01/2021 4:45:59 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietIn](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ten_tay_in] [nvarchar](200) NULL,
	[ten_loai] [nvarchar](100) NULL,
	[kho_giay] [nchar](20) NULL,
	[chinh] [float] NULL,
	[bu_thanh_pham] [float] NULL,
	[sl_tong] [float] NULL,
	[phuong_phap_in] [nchar](10) NULL,
	[so_kg] [float] NULL,
	[so_luot_in_quy_doi] [float] NULL,
	[so_luot_in_thuc_te] [float] NULL,
	[don_hang_id] [int] NULL,
 CONSTRAINT [PK_ChiTietIn] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DonHang]    Script Date: 26/01/2021 4:45:59 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DonHang](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[created_date] [datetime] NULL,
	[last_updated] [datetime] NULL,
	[status] [smallint] NULL,
	[lan_dieu_chinh] [int] NULL,
	[ma_khach_hang] [nchar](10) NULL,
	[ten_khach_hang] [nvarchar](500) NULL,
	[ma_san_pham] [nchar](100) NULL,
	[ten_san_pham] [nvarchar](500) NULL,
	[loai] [nvarchar](50) NULL,
	[kho_ngang] [decimal](3, 1) NULL,
	[kho_doc] [decimal](3, 1) NULL,
	[ten_che_ban] [nvarchar](200) NULL,
	[quy_cach_chung] [text] NULL,
	[ten_can_bo_ql] [nvarchar](200) NULL,
	[phone_number] [nchar](20) NULL,
	[email] [nchar](100) NULL,
	[ten_can_bo_kt] [nvarchar](200) NULL,
	[so_lenh_sx] [nchar](20) NULL,
	[phieu_dnsx_so] [nchar](50) NULL,
	[nv_kinh_doanh] [nvarchar](200) NULL,
	[ngay_giao_hang] [datetime] NULL,
	[quy_cach_san_pham] [text] NULL,
	[cb_thoi_gian_giao] [nvarchar](100) NULL,
	[cb_ghi_chu] [text] NULL,
	[in_thoi_gian_giao] [nvarchar](100) NULL,
	[thanh_pham_chung] [text] NULL,
	[tp_ghi_chu] [text] NULL,
	[tp_thoi_han] [nvarchar](100) NULL,
	[nha_cc] [nvarchar](200) NULL,
	[kho_tp] [nvarchar](200) NULL,
 CONSTRAINT [PK_DonHang] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[KhachHang]    Script Date: 26/01/2021 4:45:59 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KhachHang](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ma_khach_hang] [nchar](10) NULL,
	[ten_cong_ty] [nvarchar](500) NULL,
	[dia_chi] [text] NULL,
	[ma_so_thue] [nchar](20) NULL,
	[dien_thoai] [nchar](20) NULL,
	[fax] [nchar](20) NULL,
	[email] [nchar](300) NULL,
	[nguoi_dai_dien] [nvarchar](500) NULL,
	[chuc_vu] [nvarchar](200) NULL,
	[created_date] [datetime] NULL,
	[last_updated] [datetime] NULL,
 CONSTRAINT [PK_KhachHang] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MaterialType]    Script Date: 26/01/2021 4:45:59 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaterialType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MaterialTypeName] [nchar](100) NULL,
 CONSTRAINT [PK_MaterialType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Modul]    Script Date: 26/01/2021 4:45:59 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Modul](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ModulName] [nchar](500) NULL,
	[Description] [nchar](2000) NULL,
	[Url] [nchar](100) NULL,
 CONSTRAINT [PK_Modul] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Page]    Script Date: 26/01/2021 4:45:59 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Page](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[PageName] [nchar](1000) NULL,
	[Description] [nchar](2000) NULL,
	[ModulId] [int] NULL,
	[Url] [nchar](100) NULL,
 CONSTRAINT [PK_Page] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PermissionAction]    Script Date: 26/01/2021 4:45:59 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PermissionAction](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[PageId] [int] NULL,
	[RoleId] [int] NULL,
	[ActionKey] [int] NULL,
 CONSTRAINT [PK_PermissionAction] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Role]    Script Date: 26/01/2021 4:45:59 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nchar](100) NULL,
	[Description] [nchar](1000) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SanPham]    Script Date: 26/01/2021 4:45:59 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SanPham](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ma_san_pham] [nchar](20) NULL,
	[ten_san_pham] [nvarchar](500) NULL,
	[created_date] [datetime] NULL,
	[last_updated] [datetime] NULL,
 CONSTRAINT [PK_SanPham] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 26/01/2021 4:45:59 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
	[IsDeleted] [tinyint] NULL CONSTRAINT [DF_User_IsDeleted]  DEFAULT ((0)),
	[Code] [nvarchar](50) NULL,
	[FullName] [nvarchar](200) NULL,
	[UserName] [nvarchar](50) NULL,
	[PassWord] [nvarchar](50) NULL,
	[Position] [nvarchar](200) NULL,
	[UserGroupId] [int] NULL,
	[RoomId] [int] NULL,
 CONSTRAINT [PK_User_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserGroup]    Script Date: 26/01/2021 4:45:59 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserGroup](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifyBy] [int] NULL,
	[ModifyDate] [datetime] NULL,
	[IsDeleted] [tinyint] NULL CONSTRAINT [DF_UserGroup_IsDeleted]  DEFAULT ((0)),
	[UserGroupCode] [nvarchar](50) NULL,
	[UserGroupName] [nvarchar](200) NULL,
	[AccessCode] [int] NULL,
 CONSTRAINT [PK_UserGroup] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 26/01/2021 4:45:59 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NULL,
	[UserId] [int] NULL,
	[Description] [nchar](1000) NULL,
 CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[VatTu]    Script Date: 26/01/2021 4:45:59 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VatTu](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[don_hang_id] [int] NULL,
	[nhom_vat_tu_id] [int] NULL,
	[ten] [nvarchar](1000) NULL,
	[trang] [int] NULL,
	[loai_giay] [nvarchar](200) NULL,
	[dinh_luong_giay] [int] NULL,
	[ghi_chu] [nvarchar](500) NULL,
	[kieu_in_1] [nvarchar](100) NULL,
	[kieu_in_2] [nvarchar](100) NULL,
 CONSTRAINT [PK_VatTu] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Action] ON 

INSERT [dbo].[Action] ([Id], [ActionName], [Value], [Description]) VALUES (1, N'View                                                                                                ', 1, N'Xem                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     ')
INSERT [dbo].[Action] ([Id], [ActionName], [Value], [Description]) VALUES (2, N'Add                                                                                                 ', 2, N'Thêm                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    ')
INSERT [dbo].[Action] ([Id], [ActionName], [Value], [Description]) VALUES (3, N'Update                                                                                              ', 4, N'Sửa                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     ')
INSERT [dbo].[Action] ([Id], [ActionName], [Value], [Description]) VALUES (4, N'Delete                                                                                              ', 8, N'Xóa                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     ')
SET IDENTITY_INSERT [dbo].[Action] OFF
SET IDENTITY_INSERT [dbo].[Admin] ON 

INSERT [dbo].[Admin] ([id], [AdminCode], [AdminName], [UserName], [PassWord]) VALUES (1, N'admin', N'admin', N'admin', N'123456789')
SET IDENTITY_INSERT [dbo].[Admin] OFF
SET IDENTITY_INSERT [dbo].[MaterialType] ON 

INSERT [dbo].[MaterialType] ([Id], [MaterialTypeName]) VALUES (1, N'Ruột 1                                                                                              ')
INSERT [dbo].[MaterialType] ([Id], [MaterialTypeName]) VALUES (2, N'Ruột 2                                                                                              ')
SET IDENTITY_INSERT [dbo].[MaterialType] OFF
SET IDENTITY_INSERT [dbo].[Modul] ON 

INSERT [dbo].[Modul] ([id], [ModulName], [Description], [Url]) VALUES (1, N'DANH MỤC                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            ', NULL, NULL)
INSERT [dbo].[Modul] ([id], [ModulName], [Description], [Url]) VALUES (3, N'NHẬP LIỆU                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           ', NULL, NULL)
INSERT [dbo].[Modul] ([id], [ModulName], [Description], [Url]) VALUES (4, N'THỐNG KÊ                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            ', NULL, NULL)
INSERT [dbo].[Modul] ([id], [ModulName], [Description], [Url]) VALUES (5, N'QUẢN TRỊ HỆ THỐNG                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   ', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Modul] OFF
SET IDENTITY_INSERT [dbo].[Page] ON 

INSERT [dbo].[Page] ([id], [PageName], [Description], [ModulId], [Url]) VALUES (1, N'Quản lý người dùng                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      ', NULL, 1, NULL)
INSERT [dbo].[Page] ([id], [PageName], [Description], [ModulId], [Url]) VALUES (6, N'Thống kê                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                ', N'Admin                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           ', 4, NULL)
INSERT [dbo].[Page] ([id], [PageName], [Description], [ModulId], [Url]) VALUES (12, N'Quản lý quyền truy cập                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  ', NULL, 5, N'/Admin/PermissionManagerment                                                                        ')
INSERT [dbo].[Page] ([id], [PageName], [Description], [ModulId], [Url]) VALUES (13, N'Quản lý vai trò                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         ', NULL, 5, N'/Admin/RoleManagerment                                                                              ')
INSERT [dbo].[Page] ([id], [PageName], [Description], [ModulId], [Url]) VALUES (14, N'Nhập liệu                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               ', NULL, 3, N'/Material                                                                                           ')
SET IDENTITY_INSERT [dbo].[Page] OFF
SET IDENTITY_INSERT [dbo].[PermissionAction] ON 

INSERT [dbo].[PermissionAction] ([id], [PageId], [RoleId], [ActionKey]) VALUES (1, 1, 1, 15)
INSERT [dbo].[PermissionAction] ([id], [PageId], [RoleId], [ActionKey]) VALUES (2, 1, 2, 0)
INSERT [dbo].[PermissionAction] ([id], [PageId], [RoleId], [ActionKey]) VALUES (3, 1, 3, 0)
INSERT [dbo].[PermissionAction] ([id], [PageId], [RoleId], [ActionKey]) VALUES (1002, 1, 4, 0)
INSERT [dbo].[PermissionAction] ([id], [PageId], [RoleId], [ActionKey]) VALUES (1003, 2, 1, 15)
INSERT [dbo].[PermissionAction] ([id], [PageId], [RoleId], [ActionKey]) VALUES (1004, 2, 2, 0)
INSERT [dbo].[PermissionAction] ([id], [PageId], [RoleId], [ActionKey]) VALUES (1005, 2, 3, 15)
INSERT [dbo].[PermissionAction] ([id], [PageId], [RoleId], [ActionKey]) VALUES (1006, 2, 4, 0)
INSERT [dbo].[PermissionAction] ([id], [PageId], [RoleId], [ActionKey]) VALUES (1007, 3, 1, 15)
INSERT [dbo].[PermissionAction] ([id], [PageId], [RoleId], [ActionKey]) VALUES (1008, 3, 2, 5)
INSERT [dbo].[PermissionAction] ([id], [PageId], [RoleId], [ActionKey]) VALUES (1009, 3, 3, 15)
INSERT [dbo].[PermissionAction] ([id], [PageId], [RoleId], [ActionKey]) VALUES (1010, 3, 4, 0)
INSERT [dbo].[PermissionAction] ([id], [PageId], [RoleId], [ActionKey]) VALUES (1011, 4, 1, 15)
INSERT [dbo].[PermissionAction] ([id], [PageId], [RoleId], [ActionKey]) VALUES (1012, 5, 1, 15)
INSERT [dbo].[PermissionAction] ([id], [PageId], [RoleId], [ActionKey]) VALUES (1013, 6, 1, 15)
INSERT [dbo].[PermissionAction] ([id], [PageId], [RoleId], [ActionKey]) VALUES (1014, 4, 2, 0)
INSERT [dbo].[PermissionAction] ([id], [PageId], [RoleId], [ActionKey]) VALUES (1015, 5, 2, 9)
INSERT [dbo].[PermissionAction] ([id], [PageId], [RoleId], [ActionKey]) VALUES (1016, 5, 4, 0)
INSERT [dbo].[PermissionAction] ([id], [PageId], [RoleId], [ActionKey]) VALUES (1017, 6, 4, 15)
INSERT [dbo].[PermissionAction] ([id], [PageId], [RoleId], [ActionKey]) VALUES (1018, 1, 5, 0)
INSERT [dbo].[PermissionAction] ([id], [PageId], [RoleId], [ActionKey]) VALUES (1019, 12, 1, 15)
INSERT [dbo].[PermissionAction] ([id], [PageId], [RoleId], [ActionKey]) VALUES (1020, 13, 1, 15)
INSERT [dbo].[PermissionAction] ([id], [PageId], [RoleId], [ActionKey]) VALUES (1021, 2, 5, 11)
INSERT [dbo].[PermissionAction] ([id], [PageId], [RoleId], [ActionKey]) VALUES (1022, 3, 5, 5)
INSERT [dbo].[PermissionAction] ([id], [PageId], [RoleId], [ActionKey]) VALUES (1023, 13, 3, 0)
INSERT [dbo].[PermissionAction] ([id], [PageId], [RoleId], [ActionKey]) VALUES (1024, 13, 5, 0)
INSERT [dbo].[PermissionAction] ([id], [PageId], [RoleId], [ActionKey]) VALUES (1025, 12, 5, 0)
INSERT [dbo].[PermissionAction] ([id], [PageId], [RoleId], [ActionKey]) VALUES (1026, 4, 4, 15)
INSERT [dbo].[PermissionAction] ([id], [PageId], [RoleId], [ActionKey]) VALUES (1027, 7, 1, 15)
INSERT [dbo].[PermissionAction] ([id], [PageId], [RoleId], [ActionKey]) VALUES (1028, 8, 1, 15)
INSERT [dbo].[PermissionAction] ([id], [PageId], [RoleId], [ActionKey]) VALUES (1029, 9, 1, 15)
INSERT [dbo].[PermissionAction] ([id], [PageId], [RoleId], [ActionKey]) VALUES (1030, 11, 1, 15)
INSERT [dbo].[PermissionAction] ([id], [PageId], [RoleId], [ActionKey]) VALUES (1031, 10, 1, 15)
INSERT [dbo].[PermissionAction] ([id], [PageId], [RoleId], [ActionKey]) VALUES (1032, 14, 1, 15)
INSERT [dbo].[PermissionAction] ([id], [PageId], [RoleId], [ActionKey]) VALUES (1033, 14, 4, 15)
SET IDENTITY_INSERT [dbo].[PermissionAction] OFF
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([Id], [RoleName], [Description]) VALUES (1, N'Admin                                                                                               ', N'Admin                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   ')
INSERT [dbo].[Role] ([Id], [RoleName], [Description]) VALUES (4, N'Nhân viên                                                                                           ', N'Nhân viên                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               ')
INSERT [dbo].[Role] ([Id], [RoleName], [Description]) VALUES (5, N'SuperAdmin                                                                                          ', N'SuperAdmin                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              ')
SET IDENTITY_INSERT [dbo].[Role] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([id], [CreatedBy], [CreatedDate], [ModifyBy], [ModifyDate], [IsDeleted], [Code], [FullName], [UserName], [PassWord], [Position], [UserGroupId], [RoomId]) VALUES (3, NULL, NULL, NULL, NULL, 0, N'User03', N'admin', N'admin', N'123456789', N'admin', 3, NULL)
INSERT [dbo].[User] ([id], [CreatedBy], [CreatedDate], [ModifyBy], [ModifyDate], [IsDeleted], [Code], [FullName], [UserName], [PassWord], [Position], [UserGroupId], [RoomId]) VALUES (1003, NULL, NULL, NULL, NULL, 0, N'User04', N'Phạm Văn Điệp', N'dieppv', N'123456789', NULL, 1, 2)
SET IDENTITY_INSERT [dbo].[User] OFF
SET IDENTITY_INSERT [dbo].[UserGroup] ON 

INSERT [dbo].[UserGroup] ([id], [CreatedBy], [CreatedDate], [ModifyBy], [ModifyDate], [IsDeleted], [UserGroupCode], [UserGroupName], [AccessCode]) VALUES (1, NULL, NULL, NULL, NULL, 0, N'Group1', N'Nhóm người dùng bộ phận quản lý đăng ký', 1)
INSERT [dbo].[UserGroup] ([id], [CreatedBy], [CreatedDate], [ModifyBy], [ModifyDate], [IsDeleted], [UserGroupCode], [UserGroupName], [AccessCode]) VALUES (2, NULL, NULL, NULL, NULL, 0, N'Group2', N'Nhóm người dùng bộ phận quản lý giao dịch', 2)
SET IDENTITY_INSERT [dbo].[UserGroup] OFF
SET IDENTITY_INSERT [dbo].[UserRole] ON 

INSERT [dbo].[UserRole] ([Id], [RoleId], [UserId], [Description]) VALUES (3, 1, 3, N'Admin                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   ')
INSERT [dbo].[UserRole] ([Id], [RoleId], [UserId], [Description]) VALUES (9, 2, 3, N'Nhân viên                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               ')
INSERT [dbo].[UserRole] ([Id], [RoleId], [UserId], [Description]) VALUES (10, 3, 3, N'Nhân viên                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               ')
INSERT [dbo].[UserRole] ([Id], [RoleId], [UserId], [Description]) VALUES (11, 4, 3, N'Nhân viên                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               ')
INSERT [dbo].[UserRole] ([Id], [RoleId], [UserId], [Description]) VALUES (12, 5, 3, N'test                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    ')
INSERT [dbo].[UserRole] ([Id], [RoleId], [UserId], [Description]) VALUES (16, NULL, 1003, NULL)
INSERT [dbo].[UserRole] ([Id], [RoleId], [UserId], [Description]) VALUES (17, 6, 3, N'Cá nhân                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 ')
INSERT [dbo].[UserRole] ([Id], [RoleId], [UserId], [Description]) VALUES (18, 9, 3, N'3245y654                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                ')
INSERT [dbo].[UserRole] ([Id], [RoleId], [UserId], [Description]) VALUES (19, 10, 3, N'dfhdfh                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  ')
INSERT [dbo].[UserRole] ([Id], [RoleId], [UserId], [Description]) VALUES (20, 4, 1003, N'Nhân viên                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               ')
SET IDENTITY_INSERT [dbo].[UserRole] OFF
USE [master]
GO
ALTER DATABASE [Materials] SET  READ_WRITE 
GO
