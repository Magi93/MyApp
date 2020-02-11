import { Component, OnInit, Input, Output, EventEmitter } from "@angular/core";

@Component({
  selector: "app-registration",
  templateUrl: "./registration.component.html",
  styles: []
})
export class RegistrationComponent implements OnInit {
  @Input() msg: string;
  @Output() helloalert = new EventEmitter<boolean>();
  constructor() {}

  ngOnInit() {
    console.log("Hi I am Child msg from par:", this.msg);
    // this.helloalertMethod();
  }
  helloalertMethod() {
    //this.helloalert.emit(true);
    alert("hi");
  }
}
