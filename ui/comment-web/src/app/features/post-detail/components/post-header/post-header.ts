import { DatePipe } from '@angular/common';
import { ChangeDetectionStrategy, Component, input } from '@angular/core';
import { User } from '../../../../core/api/models';
import { UserAvatar } from '../../../../shared/user-avatar/user-avatar';

@Component({
  selector: 'app-post-header',
  imports: [DatePipe, UserAvatar],
  templateUrl: './post-header.html',
  styleUrl: './post-header.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PostHeader {
  readonly author = input.required<User>();
  readonly createdAt = input.required<string>();
}
