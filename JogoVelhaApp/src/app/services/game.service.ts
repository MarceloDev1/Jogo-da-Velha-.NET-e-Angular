import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface GameState {
  id: string;
  board: string[];
  currentPlayer: 'X' | 'O' | 0 | 1;
  status: 'InProgress' | 'Draw' | 'XWins' | 'OWins' | 0 | 1 | 2 | 3;
}

export interface GameResult {
  id: number;
  vencedor: 'X' | 'O' | 'E';
  dataHora: string;
}

@Injectable({ providedIn: 'root' })
export class GameService {
  private http = inject(HttpClient);

  create(): Observable<GameState> {
    return this.http.post<GameState>('/api/games', {});
  }

  move(id: string, index: number): Observable<GameState> {
    return this.http.post<GameState>(`/api/games/${id}/move`, { index });
  }

  postResult(vencedor: 'X'|'O'|'E'): Observable<GameResult> {
    return this.http.post<GameResult>('/api/games/results', { vencedor: vencedor });
  }

  getResults(): Observable<GameResult[]> {
    return this.http.get<GameResult[]>('/api/games/results');
  }
}
