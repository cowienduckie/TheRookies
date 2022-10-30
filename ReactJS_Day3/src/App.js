import logo from './logo.svg';
import './App.css';
import { useState } from 'react';
import { Counter, Welcome, Checkboxes, Pokemon, OptionSelector } from './components';

function App() {
  const options = [
    { name: "Please choose an option", value: "" },
    { name: "Welcome", value: "welcome" },
    { name: "Counter", value: "counter" },
    { name: "Checkboxes", value: "checkboxes" },
    { name: "Pokemon", value: "pokemon" }
  ];

  const [option, setOption] = useState();

  const passOption = (newOption) => {
    setOption(newOption);
  }

  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <h1>Rookies ReactJS Day 3</h1>

        <OptionSelector options={ options } passOption = { passOption }  />

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
        {option === "pokemon" &&
          <Pokemon />
        }
      </header>
    </div>
  );
}

export default App;
