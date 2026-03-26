import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IdentityService } from '../identity.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit {
  formGroup: FormGroup;
  emailModel:string='';
  constructor(private fb:FormBuilder,private _service:IdentityService,private route:Router){}
  ngOnInit(): void {
    this.formValidation()
  }

  formValidation() {
    this.formGroup = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).{8,}$/)]]
    })
  }


  get _email() {
    return this.formGroup.get('email');
  }
  get _password() {
    return this.formGroup.get('password');
  }

  Submit(){
  if(this.formGroup.valid){
    this._service.login(this.formGroup.value).subscribe({
      next:(value)=>{
        console.log(value);
        this.route.navigateByUrl('')
      },
      error:(err)=>{
        console.log(err)
      }
    })
  }
  }

  sendEmailForgetPassword(){
    this._service.forgetPassword(this.emailModel).subscribe({
      next(value){
        console.log(value)
      },
      error(err){
        console.log(err)
      },
    })
  }
}
