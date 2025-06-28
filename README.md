Quiz & Test Project
Dự án này là một hệ thống cho phép tạo quiz và test. Người dùng có thể tạo câu hỏi, tổ chức quiz, và thực hiện các bài test qua giao diện web. Backend sử dụng ASP.NET API, frontend sử dụng ReactJS, và cơ sở dữ liệu sử dụng SQL Server.

Giới thiệu
Hệ thống này giúp người dùng có thể tạo quiz và test online, bao gồm các tính năng:

Tạo và quản lý các câu hỏi (dạng question - answer)

Luyện tập với các câu hỏi đã tạo

Tạo quiz và bài kiểm tra từ các câu hỏi đã có (đang hoàn thiện)

Cài đặt

Clone repository về máy tính của bạn

Chạy file sql DbWithSomeData.sql bằng SQL Server Managerment Studio

Backend (C#)
Mở dự án bằng Visual Studio (Mở file QuizWeb.sln trong thư mục backend-repo) và chạy nó.

Cập nhật chuỗi kết nối trong file appsettings.json để kết nối với SQL Server

Frontend (ReactJS)
Vào thư mục frontend-repo:

Cài đặt các package cần thiết:
npm install

Chạy ứng dụng React:
npm start

Truy cập ứng dụng qua http://localhost:3000

Dự tính tiếp theo:
Hoàn thiện nốt phần test
Deploy dự án
