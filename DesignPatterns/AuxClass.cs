using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.AuxClass;

internal class AuxClass
{
    public void Main()
    {
        try
        {    // ...   
            MetodoQueElevaExcepcion();
            // ...
        }
        catch (Exception ex)
        {
            log.Write($"Se ha producido un error: {ex.Message}");
            throw;
        }
    }

    private void MetodoQueElevaExcepcion()
    {
        throw new NotImplementedException();
    }
}