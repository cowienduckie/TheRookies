import logo from './logo.svg';
import './App.css';
import { useState } from 'react';

function App() {
  const options = [
    { name: "Please choose an option", value: "" },
    { name: "Welcome", value: "welcome" },
    { name: "Counter", value: "counter" },
    { name: "Checkboxes", value: "checkboxes" }
  ];

  const [option, setOption] = useState();

  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <h1>Rookies ReactJS Day 2</h1>

        <select onChange={(event) => setOption(event.target.value)}>
          {
            options.map((value, index) => <option key={index} value={value.value}>{value.name}</option>)
          }
        </select>
        <span>Option selected: {option}</span>

        {option === "welcome" &&
          <>
            <Welcome name="Minh Tran" age="22" color="white" backgroundColor="red" />
            <Welcome name="Minh Tran" age="22" color="black" backgroundColor="yellow" />
            <Welcome name="Minh Tran" age="22" color="white" backgroundColor="green" />
          </>
        }
        {option === "counter" &&
          <Counter />
        }
        {option === "checkboxes" &&
          <Checkboxes />
        }
      </header>
    </div>
  );
}

function Welcome(props) {
  const welcomeStyle = {
    color: props.color,
    backgroundColor: props.backgroundColor
  };

  return (
    <div className='person-info' style={welcomeStyle}>
      <p><strong>Name: {props.name}</strong></p>
      <p>Age: {props.age}</p>
    </div>
  );
}

function Counter() {
  const [counter, setCounter] = useState(0);

  return (
    <div className='counter'>
      <button onClick={() => setCounter(counter - 1)}>-</button>
      <p>{counter}</p>
      <button onClick={() => setCounter(counter + 1)}>+</button>
    </div>
  );
}

function Checkboxes() {
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

export default App;