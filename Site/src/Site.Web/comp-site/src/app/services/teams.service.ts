import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ITeam } from '../models/team';

@Injectable({
  providedIn: 'root'
})
export class TeamsService {

  constructor(private client: HttpClient) { }

  signUp(team: ITeam) {
    return this.client.post('api/team', team);
  }
}
