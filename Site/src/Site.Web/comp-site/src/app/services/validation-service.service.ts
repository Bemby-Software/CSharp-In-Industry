import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ValidationService {

  constructor(private client: HttpClient) { 
  }

  isTeamNameInUse(name: string) {
    console.log(name);
    return this.client.get<boolean>(`api/validation/isTeamNameInUse/${name}`);
  }
}
