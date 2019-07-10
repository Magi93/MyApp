import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import * as component from "./component/index";
// const routes: Routes = [
//   {
//     path: "login",
//     component: component.UserComponent,
//     children: [{ path: "", component: component.LoginComponent }]
//   },
//   {
//     path: "register",
//     component: component.UserComponent,
//     children: [{ path: "", component: component.RegistrationComponent }]
//   },
//   {
//     path: "",
//     redirectTo: "/login",
//     pathMatch: "full"
//   }
// ];
const routes: Routes = [
  {
    path: "login",
    component: component.UserComponent,
    children: [{ path: "", component: component.LoginComponent }]
  },
  {
    path: "register",
    component: component.UserComponent,
    children: [{ path: "", component: component.RegistrationComponent }]
  },
  {
    path: "user",
    component: component.UserComponent,
    children: [
      { path: "login", component: component.LoginComponent },
      { path: "register", component: component.RegistrationComponent }
    ]
  },
  {
    path: "",
    redirectTo: "/login",
    pathMatch: "full"
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
