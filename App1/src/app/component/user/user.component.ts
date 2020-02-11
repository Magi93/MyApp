import { Component, OnInit, AfterViewInit, ViewChild } from "@angular/core";
import { RegistrationComponent } from "./registration/registration.component";
@Component({
  selector: "app-user",
  templateUrl: "./user.component.html",
  styleUrls: []
})
export class UserComponent implements OnInit, AfterViewInit {
  @ViewChild(RegistrationComponent)
  private regComp: RegistrationComponent;
  constructor() {}
  messagetoChild = "Hello";
  ngOnInit() {}
  callMethodOfChild() {
    console.log("call from parant");
    debugger;
    this.regComp.helloalertMethod();
  }
  ngAfterViewInit() {
    //this.regComp.helloalertMethod();
  }
}
