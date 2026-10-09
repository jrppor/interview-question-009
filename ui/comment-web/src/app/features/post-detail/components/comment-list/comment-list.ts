import { ChangeDetectionStrategy, Component, input, output } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { PostComment } from '../../../../core/api/models';
import { TimeAgo } from '../../../../shared/time-ago/time-ago';
import { UserAvatar } from '../../../../shared/user-avatar/user-avatar';

@Component({
  selector: 'app-comment-list',
  imports: [MatButtonModule, TimeAgo, UserAvatar],
  templateUrl: './comment-list.html',
  styleUrl: './comment-list.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CommentList {
  readonly comments = input.required<PostComment[]>();
  readonly hasMore = input(false);
  readonly loadingMore = input(false);
  readonly loadMore = output<void>();
}
