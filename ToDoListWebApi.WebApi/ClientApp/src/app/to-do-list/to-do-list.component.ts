import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ToDo } from './to-do';
import { ToDoListService } from './to-do-list.service';

@Component({
  selector: 'to-do-list',
  templateUrl: './to-do-list.component.html',
  styleUrls: ['./to-do-list.component.css']
})
export class ToDoListComponent {
  public myList: ToDo[];

  constructor(private toDoListService: ToDoListService) {
    this.getToDoList();
  }

  public getToDoList(): void {
    this.toDoListService.getToDoList().subscribe(
      result => {
        this.myList = result;
      },
      error => {
        console.error(error);
      }
    )
  }

  public add(taskName: string): void {
    const newToDo = new ToDo();
    newToDo.taskName = taskName;
    this.toDoListService.addToDo(newToDo).subscribe(
      result => {
        this.getToDoList();
      },
      error => {
        console.log(error)
      }
    );
  }

  public clear(): void {
    this.toDoListService.clearToDoList().subscribe(
      result => {
        this.getToDoList();
      },
      error => {
        console.log(error);
      }
    );
  }

  public delete(id: number): void {
    this.toDoListService.deleteToDo(id).subscribe(
      result => {
        this.getToDoList();
      },
      error => {
        console.log(error);
      }
    );
  }
}
