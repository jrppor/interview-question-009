# CommentSystem

ระบบ Comment บนโพสต์ ประกอบด้วย 2 โปรเจค

## api — CommentSystem

Backend REST API ด้วย .NET 10 แยก 4 layer: Domain, Application, Infrastructure, WebApi

- EF Core + SQL Server สำหรับเก็บ users, posts และ comments
- Endpoints หลัก: `PostsController` (ดูโพสต์, ดู/เพิ่ม comment), `UsersController` (`/api/users/me`)
- Comment แบ่งหน้าด้วย cursor (`before` + `limit`) เรียงใหม่สุดก่อน
- ผู้ comment อ่านจาก `CurrentUser:UserId` ใน appsettings (ยังไม่มี login)
- Response ห่อด้วย `ApiResponse<T>` (`success`, `message`, `errors`, `data`)
- Serilog เขียน log เป็น text file (`logs/`), Swagger, CORS

## ui — comment-web

Frontend ด้วย Angular 22 + Angular Material

- `features/post-detail` — หน้าโพสต์ (รูป, ช่องพิมพ์, รายการ comment)
  - `components/post-header`, `comment-input`, `comment-list`
- `core/api/posts-api` — เรียก API ฝั่ง backend
- `core/interceptors/error-interceptor` — แสดง snackbar เมื่อ API ผิดพลาด
- `shared/user-avatar` — วงกลม avatar สีตามชื่อ

## API

| Method | Path | หน้าที่ |
| --- | --- | --- |
| GET | `/api/posts/{postId}` | ดูโพสต์ |
| GET | `/api/posts/{postId}/comments?before=&limit=` | รายการ comment (ใหม่ → เก่า) |
| POST | `/api/posts/{postId}/comments` | เพิ่ม comment (`{ "content": "..." }`, 1-500 ตัวอักษร) |
| GET | `/api/users/me` | ผู้ใช้ปัจจุบัน |
