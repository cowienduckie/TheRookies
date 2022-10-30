import './Counter.css'
import { useState } from 'react'

export function Counter() {
  const [counter, setCounter] = useState(0);

  return (
    <div className='counter'>
      <button onClick={() => setCounter(counter - 1)}>-</button>
      <p>{counter}</p>
      <button onClick={() => setCounter(counter + 1)}>+</button>
    </div>
  );
}