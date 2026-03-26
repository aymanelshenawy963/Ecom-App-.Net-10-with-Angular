import { Component, OnInit } from '@angular/core';
import { ResetPassword } from '../../shared/Models/ResetPassword';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { IdentityService } from '../identity.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.scss'
})
export class ResetPasswordComponent implements OnInit {

  formGroup:FormGroup;
  resetValue = new ResetPassword();

  constructor(private router:ActivatedRoute,
    private fb:FormBuilder,
    private _service:IdentityService,
    private route:Router
  ){}

  ngOnInit(): void {
    this.FormValidation();

    this.router.queryParams.subscribe((param)=>{
     this.resetValue.email=param['email'];
     this.resetValue.token = decodeURIComponent(param['code']);
    });
  }

  FormValidation(){
    this.formGroup = this.fb.group({
      password: ['', [Validators.required, Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).{8,}$/)]],
      confirmPassword: ['', [Validators.required, Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).{8,}$/)]]
      },
    {validator:this.PasswordMatchValidation}
    )
  }

  PasswordMatchValidation(form:FormGroup){
    const passwordControl = form.get('password');
    const confirmPasswordControl = form.get('confirmPassword');

    if(passwordControl.value===confirmPasswordControl.value)
        return null
      else
        confirmPasswordControl?.setErrors({passwordMisMatch:true})
  }

  get _password(){
    return this.formGroup.get('password')
  }

  get _confirmPassword(){
    return this.formGroup.get('confirmPassword')
  }
Submit() {
  if (this.formGroup.valid) {

    this.resetValue.password = this.formGroup.value.password;

    this._service.resetPassword(this.resetValue).subscribe({
      next: () => {
        this.route.navigateByUrl('/account/login');
      },
      error: (err) => {
        console.log(err);
      }
    });

  }
}
}
