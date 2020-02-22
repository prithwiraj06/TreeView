import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Plan } from '../models/plan.model';

@Injectable({
  providedIn: 'root'
})

export class ProjectTreeService {
  constructor(private _httpClient: HttpClient) { }

  getProjectTree(): Observable<Plan[]> {
    return this._httpClient.get<Plan[]>('ProjectTree/GetProjectTreeData');
  }
}
