import './OptionSelector.css';
import { useState } from 'react';

export function OptionSelector(props) {
  const { options, passOption } = props;

  const [option, setOption] = useState();

  const onSelectOption = (event) => {
    passOption(event.target.value);
    setOption(event.target.value);
  }

  return (
    <div>
      <select onChange={onSelectOption}>
        {
          options.map((value, index) => <option key={index} value={value.value}>{value.name}</option>)
        }
      </select>
      <br/>
      <span>Option selected: {option}</span>
    </div>
  );
}