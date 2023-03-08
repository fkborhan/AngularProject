import { Component, OnInit } from '@angular/core';
import {
  HttpClientModule,
  HttpClient,
  HttpParams,
  HttpHeaders
} from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { DataService } from "../Data.Service";
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-dept-items-angular-component',
  providers: [DataService],
  templateUrl: './dept-items-angular-component.component.html',
  styleUrls: ['./dept-items-angular-component.component.css']
})
export class DeptItemsAngularComponentComponent implements OnInit {

  ngOnInit() {


    this.data.user
      .subscribe(res => {
        // alert(res);
        if (!!res) {
          this.data2 = res;
        }
      });

  }
  public files: any[];
  items: any;
  sl: number = 0;
  name2: string = "";
  //name2 with linked with <input name="name" id="name" #name  [value]=name2 />
  itemcode2: string = "";
  itemname2: string = '';
  deptid2: string = "";
  cost2: number = 0;
  rate2: number = 0;
  date2: string = "";
  picture2: string = "";
  deptname2: string = "";
  location2: string = "";
  data2: string = "";
  message: string = "";
  //subscription: Subscription;
  constructor(public http: HttpClient, public data: DataService, private route: ActivatedRoute) {
    this.files = [];
    this.http.get('https://localhost:7140/deptitems/GetAllItems')
      .subscribe(data => {
        this.items = data;
        console.log(this.items);
      });
    this.sl = 0;
    //console.log(this.message);
    this.route.queryParams.subscribe(params => {
      this.deptid2 = params['deptid'];
      // alert("LL")
      this.deptchange();

      //  alert(this.deptid2);
    });


  }
  deptchange() {
    this.items = [];
    this.deptname2 = "";
    this.location2 = "";
    //alert('https://localhost:7140/deptitems/GetDept/' + this.deptid2)
    this.http.get('https://localhost:7140/deptitems/GetDept/' + this.deptid2)
      .subscribe(data => {
        if (data != "") {
          this.deptname2 = Object.values(data)[0].deptname;
          this.location2 = Object.values(data)[0].location;
          this.showItems();
        }
      });
  }
  showItems() {
    // alert("here")
    this.http.get('https://localhost:7140/deptitems/GetItems/' + this.deptid2)
      .subscribe(data => {
        this.items = data;
        console.log(this.items);
      });
    this.sl = 0;
    // dataService.test = "hello";
  }
  onFileChanged(event: any) {
    this.files = event.target.files;
    const formData = new FormData();
    formData.append('files', this.files[0]);
    this.http.post('https://localhost:7140/deptitems/Post/', formData).subscribe(data => {
      this.picture2 = this.files[0].name
    });
  }
  addItems(itemcode: string, itemname: string, deptid: string, cost: string, rate: string, date: string, picture: string): void {
    this.items.push({
      itemcode: itemcode,
      itemname: itemname,
      cost: cost,
      rate: rate,
      date: date,
      picture: this.files[0].name,

    });
    this.itemname2 = '';
    this.itemcode2 = '';
    //this.deptid2 = "";
    this.cost2 = 0;
    this.rate2 = 0;
    this.date2 = "";
  }
  convertDate(inputFormat: Date) {
    function pad(s: number) { return (s < 10) ? '0' + s : s; }
    var d = new Date(inputFormat)
    return [d.getFullYear(), pad(d.getMonth() + 1), pad(d.getDate())].join('-')
  }
  show(id: number, itemcode1: string, itemname1: string, deptid1: string, cost1: number, rate1: number, date1: Date, picture1: string): void {
    this.sl = id;
    this.itemname2 = itemname1;
    this.itemcode2 = itemcode1;
    this.cost2 = cost1;
    this.rate2 = rate1;
    this.date2 = this.convertDate(new Date(date1));
    this.picture2 = picture1;

  }
  updateItems(itemcode: HTMLInputElement, itemname: HTMLInputElement, deptid: HTMLInputElement, cost: HTMLInputElement, rate: HTMLInputElement, date: HTMLInputElement): void {
    this.items[this.sl].itemcode = itemcode.value;
    this.items[this.sl].itemname = itemname.value;
    // this.items[this.sl].deptid = deptid.value;
    this.items[this.sl].cost = cost.value;
    this.items[this.sl].rate = rate.value;
    this.items[this.sl].date = date.value;
    this.itemname2 = '';
    this.itemcode2 = '';
    // this.deptid2 = "";
    this.cost2 = 0;
    this.rate2 = 0;
    this.date2 = "";


  }
  deleteItems(): void {
    this.items.splice(this.sl, 1);
    this.itemname2 = '';
    this.itemcode2 = '';
    // this.deptid2 = "";
    this.cost2 = 0;
    this.rate2 = 0;
    this.date2 = "";

  }

  deleteAll(): void {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    }
    var data = {};
    this.http.post<any>('https://localhost:7140/deptitems/RemoveDeptItemsVm/' + this.deptid2, JSON.stringify(data), httpOptions).subscribe(data => {
      window.location.href = 'https://localhost:4200/';
    });
  }

  saveAll(): void {
    var i = 0;
    var detailsArr = [];
    var dept2 = {
      deptid: this.deptid2,
      deptname: this.deptname2,
      location: this.location2
    };
    for (let value of this.items) {
      detailsArr.push({
        itemcode: value.itemcode,
        itemname: value.itemname,
        deptid: this.deptid2,
        cost: value.cost,
        rate: value.rate,
        date: value.date,
        picture: value.picture,
      });
    }
    var data = {
      "dept2": dept2,
      "items2": detailsArr
    };
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    }
    console.log(JSON.stringify(data));
    this.http.post<any>('https://localhost:7140/deptitems/AddDeptItemsVm', JSON.stringify(data), httpOptions).subscribe(data => {
      window.location.href = 'https://localhost:4200/';
    });
  }
  myalert(data: string) {
    this.data.user
      .subscribe(res => {
        // alert(res);
        if (!!res) {
          this.data2 = res;
        }
      });
  }
}

