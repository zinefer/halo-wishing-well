import React, { useEffect, useState } from "react";

import './CoinCounter.css';

import { wellCoinCount } from "../services/WishService";

export const CoinCounter = () => {
    const [coinCount, setCoinCount] = useState(0);

    useEffect(() => {
        const tmr = setInterval(() => {
            setCoinCount(wellCoinCount().count);
        }, 5000);
        return () => clearInterval(tmr);
    }, [coinCount]);

    return (
        <div className="counter"><strong>{coinCount}</strong></div>
    );
};