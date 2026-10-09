import { ChangeDetectionStrategy, Component, input, model, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { User } from '../../../../core/api/models';
import { UserAvatar } from '../../../../shared/user-avatar/user-avatar';

export const MAX_COMMENT_LENGTH = 500;

@Component({
  selector: 'app-comment-input',
  imports: [FormsModule, MatButtonModule, MatFormFieldModule, MatInputModule, UserAvatar],
  templateUrl: './comment-input.html',
  styleUrl: './comment-input.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CommentInput {
  readonly user = input.required<User>();
  readonly sending = input(false);
  readonly text = model('');
  readonly submitted = output<string>();

  protected readonly maxLength = MAX_COMMENT_LENGTH;

  protected onEnter(event: Event): void {
    // Enter that confirms an IME composition must not send the comment
    if ((event as KeyboardEvent).isComposing) {
      return;
    }

    event.preventDefault();
    this.send();
  }

  protected send(): void {
    const content = this.text().trim();
    if (!content || this.sending()) {
      return;
    }

    this.submitted.emit(content);
  }
}
