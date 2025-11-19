import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GameService, GameState } from '../../services/game.service';

@Component({
  selector: 'app-board',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.css'],
})
export class BoardComponent {
  @Input() game?: GameState;

  constructor(private api: GameService) {}

  play(i: number) {
    if (!this.game) return;
    if (this.game.status !== 'InProgress') return;
    if (this.game.board[i] !== ' ') return;

    this.api.move(this.game.id, i).subscribe((g: GameState) => {
      this.game = g;
    });
  }
}