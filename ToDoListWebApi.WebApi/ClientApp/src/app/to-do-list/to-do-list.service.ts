import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ToDo } from './to-do';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ToDoListService {

  private endPoint: string;
  private baseUrl: string;
  private http: HttpClient;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
    this.endPoint = `${this.baseUrl}todolist`;
    this.http = http;
  }

  public getToDoList(): Observable<ToDo[]> {
    return this.http.get<ToDo[]>(`${this.endPoint}/getall`);
  }

  public getSpecificToDo(id: number): Observable<ToDo> {
    return this.http.get<ToDo>(`${this.endPoint}/get/${id}`);
  }

  public addToDo(newToDo: ToDo): Observable<any> {
    return this.http.post<ToDo>(`${this.endPoint}/create`, newToDo);
  }

  public deleteToDo(id: number): Observable<any> {
    return this.http.delete(`${this.endPoint}/delete/${id}`);
  }

}
