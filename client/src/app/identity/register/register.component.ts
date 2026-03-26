import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IdentityService } from '../identity.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent implements OnInit {
  formGroup: FormGroup;

  constructor(private fb: FormBuilder,private _service:IdentityService,private toast:ToastrService,private route:Router) { }

  ngOnInit(): void {
    this.formValidation();

  }

  formValidation() {
    this.formGroup = this.fb.group({
      userName: ['', [Validators.required, Validators.minLength(6)],],
      email: ['', [Validators.required, Validators.email]],
      displayName: ['', [Validators.required]],
      password: ['', [Validators.required, Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).{8,}$/)]]
    })
  }

  get _userName() {
    return this.formGroup.get('userName');
  }
  get _email() {
    return this.formGroup.get('email');
  }
  get _displayName() {
    return this.formGroup.get('displayName');
  }
  get _password() {
    return this.formGroup.get('password');
  }
  Submit(){
   if(this.formGroup.valid){
    this._service.register(this.formGroup.value).subscribe({
      next:(value)=>{
         console.log(value);
         this.toast.success("Register success , please confirm your email","success".toUpperCase());
          this.route.navigateByUrl('/account/login')
      },
      error:(err:any)=>{
        console.log(err)
         this.toast.error(err.error.message,"error".toUpperCase())
      }
    })
   }

  }
}
