import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { Empresa } from 'src/app/models/empresa/empresa';
import { EmpresaService } from 'src/app/services/empresa.service';
import { ReservaService } from 'src/app/services/reserva.service';
import { NgForm } from '@angular/forms';
import { Reserva } from 'src/app/models/reserva/reserva';
import { ToastrService } from 'ngx-toastr';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  nome = '';
  showModal = false;
  id = '';
  empresa = {} as Empresa;
  empresas: Empresa[];
  reserva = {} as Reserva;
  reservas: Reserva[];

  constructor(
    private empresaService: EmpresaService,
    private reservaService: ReservaService,
    private router: Router,
    private toastr: ToastrService,
    private loginService: LoginService
  ) {}

  ngOnInit() {
    this.getEmpresas();
  }

  // Chama o serviço para obter todas as empresas
  getEmpresas() {
    this.empresaService.getEmpresas().subscribe((empresas: Empresa[]) => {
      this.empresas = empresas;
    });
  }

  saveReserva(form: NgForm) {
    if (this.reserva.id !== undefined) {
      this.reservaService.updateReserva(this.reserva).subscribe(() => {
        this.router.navigate(['/reservas']);
      });
    } else {
      this.reserva.empresaId = this.id;
      this.reservaService.saveReserva(this.reserva).subscribe(() => {
        this.showModal = false;
        this.router.navigate(['/reservas']);
        this.toastr.success('Agendamento realizado com sucesso!');
      });
    }
  }

  setId(id: string) {
    this.id = id;
    if (!this.loginService.loggedIn()) {
      this.router.navigate(['/login']);
    }
  }

  buscar() {
    if (this.nome !== '') {
      this.empresas = this.empresas.filter((res) => {
        return res.nome
          .toLocaleLowerCase()
          .match(this.nome.toLocaleLowerCase());
      });
    } else if (this.nome === '') {
      this.ngOnInit();
    }
  }
}
