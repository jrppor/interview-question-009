import { rxResource } from '@angular/core/rxjs-interop';
import {
  ChangeDetectionStrategy,
  Component,
  computed,
  inject,
  input,
  linkedSignal,
  signal,
} from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { finalize, forkJoin } from 'rxjs';
import { PostComment } from '../../core/api/models';
import { PostsApi } from '../../core/api/posts-api';
import { CommentInput } from './components/comment-input/comment-input';
import { CommentList } from './components/comment-list/comment-list';
import { PostHeader } from './components/post-header/post-header';

@Component({
  selector: 'app-post-detail',
  imports: [MatButtonModule, MatProgressBarModule, PostHeader, CommentInput, CommentList],
  templateUrl: './post-detail.html',
  styleUrl: './post-detail.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PostDetail {
  private readonly api = inject(PostsApi);

  // bound from the :postId route param
  readonly postId = input.required<string>();

  protected readonly initial = rxResource({
    params: () => Number(this.postId()),
    stream: ({ params: postId }) =>
      forkJoin({
        post: this.api.getPost(postId),
        page: this.api.getComments(postId),
        user: this.api.getCurrentUser(),
      }),
  });

  protected readonly post = computed(() =>
    this.initial.hasValue() ? this.initial.value().post : null,
  );
  protected readonly currentUser = computed(() =>
    this.initial.hasValue() ? this.initial.value().user : null,
  );

  // local copies so new and older comments can be added without reloading the page
  protected readonly comments = linkedSignal<PostComment[]>(() =>
    this.initial.hasValue() ? this.initial.value().page.items : [],
  );
  protected readonly nextCursor = linkedSignal<number | null>(() =>
    this.initial.hasValue() ? this.initial.value().page.nextCursor : null,
  );

  protected readonly draft = signal('');
  protected readonly sending = signal(false);
  protected readonly loadingMore = signal(false);

  protected addComment(content: string): void {
    this.sending.set(true);

    this.api
      .addComment(Number(this.postId()), content)
      .pipe(finalize(() => this.sending.set(false)))
      .subscribe({
        next: (comment) => {
          this.comments.update((items) => [comment, ...items]);
          this.draft.set('');
        },
        // the error interceptor already shows the message; the draft stays so it can be resent
        error: () => undefined,
      });
  }

  protected loadMore(): void {
    this.loadingMore.set(true);

    this.api
      .getComments(Number(this.postId()), this.nextCursor())
      .pipe(finalize(() => this.loadingMore.set(false)))
      .subscribe({
        next: (page) => {
          this.comments.update((items) => [...items, ...page.items]);
          this.nextCursor.set(page.nextCursor);
        },
        error: () => undefined,
      });
  }
}
