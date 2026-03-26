import { AfterViewInit, Component, Inject, OnInit, PLATFORM_ID } from '@angular/core';
import { ActiveAccount } from '../../shared/Models/ActiveAccount';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { IdentityService } from '../identity.service';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs';
import { isPlatformBrowser } from '@angular/common';

@Component({
  selector: 'app-active',
  templateUrl: './active.component.html',
  styleUrl: './active.component.scss'
})
export class ActiveComponent implements OnInit {

  constructor(private router:ActivatedRoute,
    private _service:IdentityService,
    private toast:ToastrService,
    private route:Router,
    @Inject(PLATFORM_ID) private platformId: Object
  ){}

ngOnInit(): void {
  if (!isPlatformBrowser(this.platformId)) return;

  this.router.queryParams.pipe(take(1)).subscribe(param => {
    const activeParam = {
      email: param['email'],
      token: param['code']
    };

    this._service.active(activeParam).subscribe({
      next: () => {this.toast.success("Your account is active", "SUCCESS"), this.route.navigateByUrl('/account/login')},
      error: () => this.toast.error("Your account is not active", "ERROR")
    });


  });
}

}
