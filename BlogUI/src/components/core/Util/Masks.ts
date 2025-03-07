export const cpfMask = (value?: string) => {
  if (!value) return ''; // Retorna uma string vazia se o valor for undefined ou null

  return value
    .replace(/\D/g, '') 
    .replace(/(\d{3})(\d)/, '$1.$2')
    .replace(/(\d{3})(\d)/, '$1.$2')
    .replace(/(\d{3})(\d{1,2})/, '$1-$2')
    .replace(/(-\d{2})\d+?$/, '$1');
};


export const celMask = (value: string) => {
    return value
      .replace(/\D/g, '')
      .replace(/(\d{2})(\d)/,"($1) $2")
      .replace(/(\d)(\d{4})$/,"$1-$2")
}

export const cnpjMask = (value: string) => {
    return value
      .replace(/\D/g, '')                                 
      .replace(/(\d{2})(\d)/, "$1.$2")                   
      .replace(/(\d{3})(\d)/, "$1.$2")                    
      .replace(/(\d{3})(\d)/, "$1/$2")                   
      .replace(/(\d{4})(\d{2})$/, "$1-$2");               
}

export const cepMask = (value: string) => {
    return value
      .replace(/\D/g, '')               
      .replace(/(\d{5})(\d)/, "$1-$2"); 
}

export const numberMask = (value: string) => {
    return value.replace(/\D/g, '');
}