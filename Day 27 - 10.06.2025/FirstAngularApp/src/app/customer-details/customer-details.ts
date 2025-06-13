import { Component } from '@angular/core';

@Component({
  selector: 'app-customer-details',
  imports: [],
  templateUrl: './customer-details.html',
  styleUrl: './customer-details.css'
})
export class CustomerDetails {
customerDetail = {
  Name:"Srikanth",
  Email:"srikath@gmail.com",
  Phonenumber: "9894596552"
}

likeCount:number = 0;
disLikeCount:number = 0;

onClickLike(){
  ++this.likeCount;
}

onClickdisLike(){
  ++this.disLikeCount;
}

}
