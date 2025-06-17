import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-about',
  imports: [RouterOutlet],
  templateUrl: './about.html',
  styleUrl: './about.css'
})
export class About implements OnInit  {
  username:string = "";
  route = inject(ActivatedRoute);

  ngOnInit():void{
    this.username = this.route.snapshot.params["un"] as string;
  }
}
