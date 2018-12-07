using Slic3rSVG2Photon.PhotonFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slic3rSVG2Photon
{
    class PhotonFileContainer
    {
        private PFHeader header;
        private List<PFPreview> preview;
        private List<PFLayerMeta> layersMeta;
        private List<PFLayerRaw> layersRaw;
    }
}
