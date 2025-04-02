// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.RaceImageCache
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace DistantWorlds.Types
{
    public class RaceImageCache
    {
        private Bitmap[] _RaceImages;
        private Bitmap[] _RaceImagesAlt;
        private Bitmap[] _RaceImages30;
        private Bitmap[] _RaceImages30Transparent;
        private Bitmap[] _RaceImagesSmall;
        private Bitmap[] _PirateImages;
        private Bitmap[] _PirateImagesAlt;
        private Bitmap[] _PirateImages30;
        private Bitmap[] _PirateImages30Transparent;
        private Bitmap[] _PirateImagesSmall;

        public void Initialize(
          Bitmap[] raceImages,
          Bitmap[] raceImagesAlt,
          Bitmap[] pirateImages,
          Bitmap[] pirateImagesAlt)
        {
            this.Clear();
            List<Task> taskList = new List<Task>();
            this._RaceImages = raceImages;
            this._RaceImagesAlt = raceImagesAlt;
            this._RaceImagesSmall = new Bitmap[this._RaceImages.Length];
            this._RaceImages30 = new Bitmap[this._RaceImages.Length];
            this._RaceImages30Transparent = new Bitmap[this._RaceImages.Length];
            for (int index = 0; index < raceImages.Length; ++index)
            {
                int localIndex = index;
                taskList.Add(Task.Run(() =>
                {
                    this._RaceImagesSmall[localIndex] = GraphicsHelper.ScaleImage(raceImages[localIndex], 24, 24, 1f);
                    this._RaceImages30[localIndex] = GraphicsHelper.ScaleImage(raceImages[localIndex], 30, 30, 1f);
                    this._RaceImages30Transparent[localIndex] = GraphicsHelper.ScaleImage(raceImages[localIndex], 30, 30, 0.67f);
                }));
            }
            this._PirateImages = pirateImages;
            this._PirateImagesAlt = pirateImagesAlt;
            this._PirateImagesSmall = new Bitmap[this._PirateImages.Length];
            this._PirateImages30 = new Bitmap[this._PirateImages.Length];
            this._PirateImages30Transparent = new Bitmap[this._PirateImages.Length];
            for (int index = 0; index < pirateImages.Length; ++index)
            {
                int localIndex = index;
                taskList.Add(Task.Run(() =>
                {
                    this._PirateImagesSmall[localIndex] = GraphicsHelper.ScaleImage(pirateImages[localIndex], 24, 24, 1f);
                    this._PirateImages30[localIndex] = GraphicsHelper.ScaleImage(pirateImages[localIndex], 30, 30, 1f);
                    this._PirateImages30Transparent[localIndex] = GraphicsHelper.ScaleImage(pirateImages[localIndex], 30, 30, 0.67f);
                }));
            }
            Task.WaitAll(taskList.ToArray());
        }

        public Bitmap GetRaceImageSize30(int racePictureRef, bool useTransparent) => this.GetRaceImage(racePictureRef, false, false, true, useTransparent);

        public Bitmap GetRaceImage(int racePictureRef) => this.GetRaceImage(racePictureRef, false, false);

        public Bitmap GetRaceImage(int racePictureRef, bool useSmall, bool useAlternate) => this.GetRaceImage(racePictureRef, useSmall, useAlternate, false, false);

        private Bitmap GetRaceImage(
          int racePictureRef,
          bool useSmall,
          bool useAlternate,
          bool useSize30,
          bool useTransparent)
        {
            if (useSmall)
            {
                if (racePictureRef >= 0 && racePictureRef < this._RaceImagesSmall.Length)
                    return this._RaceImagesSmall[racePictureRef];
            }
            else if (useSize30)
            {
                if (useTransparent)
                {
                    if (racePictureRef >= 0 && racePictureRef < this._RaceImages30Transparent.Length)
                        return this._RaceImages30Transparent[racePictureRef];
                }
                else if (racePictureRef >= 0 && racePictureRef < this._RaceImages30.Length)
                    return this._RaceImages30[racePictureRef];
            }
            else if (useAlternate)
            {
                if (racePictureRef >= 0 && racePictureRef < this._RaceImagesAlt.Length)
                    return this._RaceImagesAlt[racePictureRef];
            }
            else if (racePictureRef >= 0 && racePictureRef < this._RaceImages.Length)
                return this._RaceImages[racePictureRef];
            return (Bitmap)null;
        }

        public int RaceImagesLength => this._RaceImages != null ? this._RaceImages.Length : 0;

        public Bitmap[] GetRaceImages() => this._RaceImages;

        public Bitmap GetEmpireDominantRaceImageSize30(Empire empire) => this.GetEmpireDominantRaceImageSize30(empire, false);

        public Bitmap GetEmpireDominantRaceImageSize30(Empire empire, bool useTransparent) => this.GetEmpireDominantRaceImage(empire, false, false, false, true, useTransparent);

        public Bitmap GetPirateImage(PiratePlayStyle piratePlaystyle)
        {
            int index = 0;
            switch (piratePlaystyle)
            {
                case PiratePlayStyle.Balanced:
                    index = 0;
                    break;
                case PiratePlayStyle.Pirate:
                    index = 1;
                    break;
                case PiratePlayStyle.Mercenary:
                    index = 2;
                    break;
                case PiratePlayStyle.Smuggler:
                    index = 3;
                    break;
                case PiratePlayStyle.Legendary:
                    index = 4;
                    break;
            }
            return index >= 0 && index < this._PirateImages.Length ? this._PirateImages[index] : (Bitmap)null;
        }

        public Bitmap GetEmpireDominantRaceImage(Empire empire) => this.GetEmpireDominantRaceImage(empire, false, false, false);

        public Bitmap GetEmpireDominantRaceImage(
          Empire empire,
          bool useSmallSize,
          bool useAlternate,
          bool useRaceWhenPirate)
        {
            return this.GetEmpireDominantRaceImage(empire, useSmallSize, useAlternate, useRaceWhenPirate, false, false);
        }

        private Bitmap GetEmpireDominantRaceImage(
          Empire empire,
          bool useSmallSize,
          bool useAlternate,
          bool useRaceWhenPirate,
          bool useSize30,
          bool useTransparent)
        {
            if (empire != null)
            {
                if (empire.PirateEmpireBaseHabitat != null && !useRaceWhenPirate)
                {
                    int index = 0;
                    switch (empire.PiratePlayStyle)
                    {
                        case PiratePlayStyle.Balanced:
                            index = 0;
                            break;
                        case PiratePlayStyle.Pirate:
                            index = 1;
                            break;
                        case PiratePlayStyle.Mercenary:
                            index = 2;
                            break;
                        case PiratePlayStyle.Smuggler:
                            index = 3;
                            break;
                        case PiratePlayStyle.Legendary:
                            if (_PirateImagesSmall.Length > 4)
                            { index = 4; }
                            else
                            { index = 0; }
                            break;
                    }
                    if (useSmallSize)
                    {
                        if (index >= 0 && index < this._PirateImagesSmall.Length)
                            return this._PirateImagesSmall[index];
                    }
                    else if (useSize30)
                    {
                        if (useTransparent)
                        {
                            if (index >= 0 && index < this._PirateImages30Transparent.Length)
                                return this._PirateImages30Transparent[index];
                        }
                        else if (index >= 0 && index < this._PirateImages30.Length)
                            return this._PirateImages30[index];
                    }
                    else if (useAlternate)
                    {
                        if (index >= 0 && index < this._PirateImagesAlt.Length)
                            return this._PirateImagesAlt[index];
                    }
                    else if (index >= 0 && index < this._PirateImages.Length)
                        return this._PirateImages[index];
                }
                else if (empire.DominantRace != null)
                {
                    int pictureRef = empire.DominantRace.PictureRef;
                    if (useSmallSize)
                    {
                        if (pictureRef >= 0 && pictureRef < this._RaceImagesSmall.Length)
                            return this._RaceImagesSmall[pictureRef];
                    }
                    else if (useSize30)
                    {
                        if (useTransparent)
                        {
                            if (pictureRef >= 0 && pictureRef < this._RaceImages30Transparent.Length)
                                return this._RaceImages30Transparent[pictureRef];
                        }
                        else if (pictureRef >= 0 && pictureRef < this._RaceImages30.Length)
                            return this._RaceImages30[pictureRef];
                    }
                    else if (useAlternate)
                    {
                        if (pictureRef >= 0 && pictureRef < this._RaceImagesAlt.Length)
                            return this._RaceImagesAlt[pictureRef];
                    }
                    else if (pictureRef >= 0 && pictureRef < this._RaceImages.Length)
                        return this._RaceImages[pictureRef];
                }
            }
            return (Bitmap)null;
        }

        public void Clear()
        {
            this.ClearImageArray(this._RaceImages);
            this.ClearImageArray(this._RaceImagesAlt);
            this.ClearImageArray(this._RaceImagesSmall);
            this.ClearImageArray(this._RaceImages30);
            this.ClearImageArray(this._PirateImages);
            this.ClearImageArray(this._PirateImagesAlt);
            this.ClearImageArray(this._PirateImagesSmall);
            this.ClearImageArray(this._PirateImages30);
        }

        private void ClearImageArray(Bitmap[] images)
        {
            if (images != null)
            {
                for (int index = 0; index < images.Length; ++index)
                {
                    if (images[index] != null)
                    {
                        if (images[index].PixelFormat != PixelFormat.Undefined)
                            images[index].Dispose();
                        images[index] = (Bitmap)null;
                    }
                }
            }
            images = (Bitmap[])null;
        }
    }
}
