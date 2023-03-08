import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable()
export class DataService {
 //public user: string;
 
  public user$ = new BehaviorSubject<string>("Hi");
  public user = this.user$.asObservable();

  
  //private messageSource = new BehaviorSubject('hello world');
  //currentMessage = this.messageSource.asObservable();




  constructor() { }
  public setData(data: string) {
    this.user$.next(data);
   // alert("dat:" + this.user$);
  }
  //changeMessage(message: string) {
  // // this.user = message;
  // alert(this.user)
  //}

}
