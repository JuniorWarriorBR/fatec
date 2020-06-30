import { Usuario } from '../usuario/usuario';
import { Reserva } from '../reserva/reserva';
import { Data } from '@angular/router';

export interface Empresa {
  id: string;
  usuarioId: string;
  nome: string;
  descricao: string;
  reservas: [
    {
      id: string;
      usuarioId: string;
      usuario: Usuario[];
      empresaId: string;
      concluida: boolean;
      dataHoraReserva: Date;
    }
  ];
  usuario: Usuario[];
}
