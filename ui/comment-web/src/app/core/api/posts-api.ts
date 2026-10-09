import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable, map } from 'rxjs';
import { environment } from '../../../environments/environment';
import { ApiResponse, unwrap } from './api-response.model';
import { CommentPage, Post, PostComment, User } from './models';

@Injectable({ providedIn: 'root' })
export class PostsApi {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.apiBaseUrl}/api`;

  getCurrentUser(): Observable<User> {
    return this.http.get<ApiResponse<User>>(`${this.baseUrl}/users/me`).pipe(map(unwrap));
  }

  getPost(postId: number): Observable<Post> {
    return this.http.get<ApiResponse<Post>>(`${this.baseUrl}/posts/${postId}`).pipe(map(unwrap));
  }

  getComments(postId: number, before?: number | null, limit = 20): Observable<CommentPage> {
    let params = new HttpParams().set('limit', limit);
    if (before != null) {
      params = params.set('before', before);
    }

    return this.http
      .get<ApiResponse<CommentPage>>(`${this.baseUrl}/posts/${postId}/comments`, { params })
      .pipe(map(unwrap));
  }

  addComment(postId: number, content: string): Observable<PostComment> {
    return this.http
      .post<ApiResponse<PostComment>>(`${this.baseUrl}/posts/${postId}/comments`, { content })
      .pipe(map(unwrap));
  }
}
