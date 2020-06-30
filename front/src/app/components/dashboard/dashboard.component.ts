import { Component, OnInit } from '@angular/core';
import { Empresa } from 'src/app/models/empresa/empresa';
import { Reserva } from 'src/app/models/reserva/reserva';
import { ReservaService } from 'src/app/services/reserva.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent implements OnInit {
  reserva = {} as Reserva;
  reservas: Reserva[];

  constructor(
    private reservaService: ReservaService,
    private toastr: ToastrService
  ) {}

  ngOnInit() {
    this.getEmpresa();
  }

  getEmpresa() {
    this.reservaService.getReservaEmpresa().subscribe((reservas: Reserva[]) => {
      this.reservas = reservas;
    });
  }

  updateReserva(id) {
    this.reserva.id = id;
    this.reserva.concluida = true;
    this.reservaService.updateReserva(this.reserva).subscribe(() => {
      this.getEmpresa();
      this.toastr.success('Agendamento encerrado com sucesso!');
    });
  }
}
