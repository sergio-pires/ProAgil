export interface Lote {
    id: number;
    nome: string;
    preco: number;
    dataIni?: Date; 
    dataFim?: Date;
    quantidade: number;
    eventoId: number;
}
