import { ChangeDetectionStrategy, Component, computed, input } from '@angular/core';

@Component({
  selector: 'app-user-avatar',
  templateUrl: './user-avatar.html',
  styleUrl: './user-avatar.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class UserAvatar {
  readonly name = input.required<string>();
  readonly url = input<string | null>(null);
  readonly size = input(40);

  protected readonly initial = computed(
    () => Array.from(this.name().trim())[0]?.toUpperCase() ?? '?',
  );

  // same name always gets the same colour
  protected readonly hue = computed(() => {
    let hash = 0;
    for (const char of this.name()) {
      hash = (hash * 31 + char.charCodeAt(0)) % 360;
    }
    return hash;
  });
}
