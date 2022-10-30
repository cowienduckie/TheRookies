import './Checkboxes.css'
import { useState } from 'react';

export function Checkboxes() {
  const options = [
    { name: "Coding", value: "coding", checked: false },
    { name: "Music", value: "music", checked: false },
    { name: "Reading Books", value: "book", checked: false }
  ];

  const [interest, setInterest] = useState(options);
  const [all, setAll] = useState(false);

  function onChangeHandler(event, index) {
    const isChecked = event.target.checked;

    setInterest(states => {
      states[index].checked = isChecked;

      return [...states];
    });

    if (isChecked === false && all === true) {
      setAll(false);
    }
    else if (isChecked === true && all === false) {
      const others = [...interest];

      others.splice(index, 1);
      
      const isAllChecked = others.reduce((prev, curr) => prev && curr.checked, true);

      setAll(isAllChecked);
    }
  }

  function setAllState(event) {
    const isChecked = event.target.checked;

    setAll(isChecked);

    setInterest(states => {
      states.forEach(value => value.checked = isChecked)

      return [...states];
    });
  }

  return (
    <div>
      <br />
      <span>Choose your interests:</span><br />
      <div className='checkboxes'>
        <label>
          <input
            name='All'
            type="checkbox"
            checked={all}
            onChange={(event) => setAllState(event)} />
          All
        </label>
        <br />
        {
          options.map((value, index) =>
            <label key={index}>
              <input
                name={value.value}
                type="checkbox"
                checked={interest[index].checked}
                onChange={(event) => onChangeHandler(event, index)} />
              {value.name}
            </label>
          )
        }
      </div>
      <br />
      <div className='selected-checkboxes'>
        <p>You selected:</p>
        {
          interest.map((value, index) =>
            <p key={index} style={{ color: value.checked ? 'green' : 'red' }}>
              {value.name}
            </p>)
        }
      </div>
    </div>
  );
}