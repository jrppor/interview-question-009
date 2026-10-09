import { Routes } from '@angular/router';

export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'posts/1' },
  {
    path: 'posts/:postId',
    loadComponent: () => import('./features/post-detail/post-detail').then((m) => m.PostDetail),
  },
];
