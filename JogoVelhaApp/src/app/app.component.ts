import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { GameService, GameState, GameResult } from './services/game.service';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  game?: GameState;
  playerX = '';
  playerO = '';
  askNames = false;

  results: GameResult[] = [];

  constructor(private api: GameService) {}

  ngOnInit(): void {
    this.loadResults();
  }

  private loadResults() {
    this.api.getResults().subscribe(r => this.results = r);
  }

  startFlow() {
    this.askNames = true;
    this.game = undefined;
  }

  confirmNames() {
    const x = this.playerX?.trim() || 'Jogador X';
    const o = this.playerO?.trim() || 'Jogador O';
    this.api.create().subscribe(g => {
      this.game = g;
      this.playerX = x;
      this.playerO = o;
      this.askNames = false;
    });
  }

    cancelNames() {
    this.askNames = false;
  }

  play(i: number) {
    if (!this.game) return;

    const s: any = this.game.status;
    const inProgress = (s === 'InProgress' || s === 0);
    const cell = this.game.board[i];
    const isEmpty = (cell === ' ' || cell === '' || cell == null);
    if (!inProgress || !isEmpty) return;

    this.api.move(this.game.id, i).subscribe(g => {
      this.game = g;

      const v = this.mapWinner(g);
      if (v) {
        this.api.postResult(v)
          .pipe(finalize(() => this.loadResults()))
          .subscribe();
      }
    });
  }

  private mapWinner(g: GameState): 'X'|'O'|'E'|null {
    const s: any = g.status;
    if (s === 'XWins' || s === 2) return 'X';
    if (s === 'OWins' || s === 3) return 'O';
    if (s === 'Draw'  || s === 1) return 'E';
    return null;
  }

  labelStatus(g?: GameState): string {
    if (!g) return '';
    const s: any = g.status;
    if (s === 'InProgress' || s === 0) {
      const curr = (g.currentPlayer === 'X' || g.currentPlayer === 0) ? 'X' : 'O';
      const name = curr === 'X' ? (this.playerX || 'Jogador X') : (this.playerO || 'Jogador O');
      return `Vez de ${name} (${curr})`;
    }
    if (s === 'Draw' || s === 1)  return 'Empate!';
    if (s === 'XWins' || s === 2) return `${this.playerX || 'Jogador X'} (X) venceu!`;
    if (s === 'OWins' || s === 3) return `${this.playerO || 'Jogador O'} (O) venceu!`;
    return '';
  }
}