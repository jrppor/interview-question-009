export interface User {
  userId: number;
  displayName: string;
  avatarUrl: string | null;
}

export interface Post {
  postId: number;
  imageUrl: string;
  caption: string | null;
  createdAt: string;
  author: User;
}

export interface PostComment {
  commentId: number;
  content: string;
  createdAt: string;
  user: User;
}

export interface CommentPage {
  items: PostComment[];
  nextCursor: number | null;
}
