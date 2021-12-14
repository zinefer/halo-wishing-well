import React, { useState } from "react";

import './Coin.css';
import { tossCoinIntoWell } from '../services/WishService'

export const Coin = () => {
  const [animClass, setAnimClass] = useState(0);

  const reset = event => {
    setAnimClass('');
  };

  const makeAWish = event => {
    tossCoinIntoWell();
    setAnimClass('clicked');
  }

  return (
    <div className={'coin ' + animClass} onClick={makeAWish} onAnimationEnd={reset}></div>
  );
};
