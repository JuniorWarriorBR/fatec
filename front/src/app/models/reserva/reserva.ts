import { Empresa } from '../empresa/empresa';
import { Usuario } from '../usuario/usuario';

export interface Reserva {
  id: string;
  usuarioId: string;
  usuario: Usuario[];
  empresaId: string;
  empresa: Empresa[];
  concluida: boolean;
  dataHoraReserva: Date;
}
