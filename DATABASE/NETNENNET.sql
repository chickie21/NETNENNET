CREATE DATABASE NETNENNET
GO

CREATE TABLE tblDONGIA (
    MaDonGia VARCHAR(10) PRIMARY KEY,
    DonGia MONEY NOT NULL
)
GO

INSERT INTO tblDONGIA VALUES ('DG001', 10000)
INSERT INTO tblDONGIA VALUES ('DG002', 20000)
INSERT INTO tblDONGIA VALUES ('DG003', 30000)
GO


CREATE TABLE tblPHONG (
    MaPhong VARCHAR(10) PRIMARY KEY,
    TenPhong NVARCHAR(50) NOT NULL,
    MaDonGia VARCHAR(10) NOT NULL,
    FOREIGN KEY (MaDonGia) REFERENCES tblDONGIA(MaDonGia)
)
GO

INSERT INTO tblPHONG VALUES ('P001', 'BASIC', 'DG001')
INSERT INTO tblPHONG VALUES ('P002', 'VIP', 'DG002')
INSERT INTO tblPHONG VALUES ('P003', 'SVIP', 'DG003')
GO

CREATE TABLE tblDUNGLUONG(
    MaDungLuong VARCHAR(10) PRIMARY KEY,
    DungLuong INT NOT NULL
)
GO


INSERT INTO tblDUNGLUONG VALUES ('RAM001', 8)
INSERT INTO tblDUNGLUONG VALUES ('RAM002', 16)
INSERT INTO tblDUNGLUONG VALUES ('RAM003', 32)
INSERT INTO tblDUNGLUONG VALUES ('ROM001', 128)
INSERT INTO tblDUNGLUONG VALUES ('ROM002', 256)
INSERT INTO tblDUNGLUONG VALUES ('ROM003', 512)
GO

CREATE TABLE tblOCUNG (
    MaOCung VARCHAR(10) PRIMARY KEY,
    TenOCung NVARCHAR(50) NOT NULL,
    MaDungLuong VARCHAR(10) NOT NULL,
    FOREIGN KEY (MaDungLuong) REFERENCES tblDUNGLUONG(MaDungLuong)
)
GO


INSERT INTO tblOCUNG VALUES ('OC001', 'Kingston', 'ROM003')
INSERT INTO tblOCUNG VALUES ('OC002', 'WD', 'ROM002')
INSERT INTO tblOCUNG VALUES ('OC003', 'Seagate', 'ROM003')
GO

CREATE TABLE tblCHIP (
    MaChip VARCHAR(10) PRIMARY KEY,
    TenChip NVARCHAR(50) NOT NULL
)
GO

INSERT INTO tblCHIP VALUES ('CHIP001', 'Intel Core i9 9900K')
INSERT INTO tblCHIP VALUES ('CHIP002', 'AMD Ryzen 7 3700X')
INSERT INTO tblCHIP VALUES ('CHIP003', 'AMD Ryzen 9 3900X')
GO

CREATE TABLE tblRAM (
    MaRAM VARCHAR(10) PRIMARY KEY,
    TenRAM NVARCHAR(50) NOT NULL,
    MaDungLuong VARCHAR(10) NOT NULL,
    FOREIGN KEY (MaDungLuong) REFERENCES tblDUNGLUONG(MaDungLuong)
)
GO

INSERT INTO tblRAM VALUES ('RAM001', 'Kingston', 'RAM001')
INSERT INTO tblRAM VALUES ('RAM002', 'Corsair', 'RAM002')
GO

CREATE TABLE tblMANHINH (
    MaManHinh VARCHAR(10) PRIMARY KEY,
    TenManHinh NVARCHAR(50) NOT NULL
)
GO

INSERT INTO tblMANHINH VALUES ('MH001', 'BenQ')
INSERT INTO tblMANHINH VALUES ('MH002', 'ViewSonic')
INSERT INTO tblMANHINH VALUES ('MH003', 'EDRA')
GO


CREATE TABLE tblCOMANHINH (
    MaCoManHinh VARCHAR(10) PRIMARY KEY,
    TenCoManHinh NVARCHAR(50) NOT NULL
)
GO

INSERT INTO tblCOMANHINH VALUES ('CMH001', '24 inch')
INSERT INTO tblCOMANHINH VALUES ('CMH002', '27 inch')
INSERT INTO tblCOMANHINH VALUES ('CMH003', '32 inch')
GO

CREATE TABLE tblCHUOT (
    MaChuot VARCHAR(10) PRIMARY KEY,
    TenChuot NVARCHAR(50) NOT NULL
)
GO

INSERT INTO tblCHUOT VALUES ('CH001', 'Logitech')
INSERT INTO tblCHUOT VALUES ('CH002', 'Corsair')
INSERT INTO tblCHUOT VALUES ('CH003', 'Zowie')
GO


CREATE TABLE tblBANPHIM (
    MaBanPhim VARCHAR(10) PRIMARY KEY,
    TenBanPhim NVARCHAR(50) NOT NULL
)
GO

INSERT INTO tblBANPHIM VALUES ('BP001', 'Logitech')
INSERT INTO tblBANPHIM VALUES ('BP002', 'Corsair')
INSERT INTO tblBANPHIM VALUES ('BP003', 'Zowie')
GO


CREATE TABLE tblLOA (
    MaLoa VARCHAR(10) PRIMARY KEY,
    TenLoa NVARCHAR(50) NOT NULL
)
GO

INSERT INTO tblLOA VALUES ('LOA002', 'Marshall')
INSERT INTO tblLOA VALUES ('LOA003', 'JBL')
INSERT INTO tblLOA VALUES ('LOA004', 'Bose')
GO

