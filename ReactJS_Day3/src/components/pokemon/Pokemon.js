import './Pokemon.css';
import axios from 'axios';
import { useState, useEffect } from 'react';

export function Pokemon() {
  const initialId = 1;
  const [pokemonId, setPokemonId] = useState(initialId);
  const [pokemonInfo, setPokemonInfo] = useState({
    id: '',
    name: '',
    weight: '',
    icons: []
  });
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState({
    isError: false,
    msg: ''
  });

  useEffect(
    () => {
      setLoading(true);

      axios.get(`https://pokeapi.co/api/v2/pokemon/${pokemonId}`)
      .then(result => {
        setPokemonInfo(info => {
          info.id = result.data.id;
          info.name = result.data.name;
          info.weight = result.data.weight;
          info.icons = [];
          
          for (let prop in result.data.sprites) {
            if (typeof prop === 'string') {
              info.icons.push(result.data.sprites[prop]);
            }
          }
          
          return {...info};
        });
        setError({
          isError: false,
          msg: '' 
        });
        setLoading(false);
      })
      .catch(error => {
        console.log(error);
        setError({
          isError: true,
          msg: error.message
        });
        setLoading(false);
      });
    }, 
    [pokemonId]);

  function PokemonInfo() {
    return (
      <div className='pokemon-info'>
        <p>Id: { pokemonInfo.id }</p>
        <p>Name: { pokemonInfo.name }</p>
        <p>Weight: { pokemonInfo.weight }</p>
        <div className='pokemon-icons'>
          {
            pokemonInfo.icons.map((value, index) => <img key={index} src={value} alt=''/>)
          }
        </div>
      </div>
    )
  };

  return (
    <div>
      { loading && <div className='loading'></div> }
      { !loading && !error.isError && <PokemonInfo /> }
      { !loading && error.isError && <span className='error-msg'>{ error.msg }</span> }
      
      <div className='controls'>
        <button 
          onClick={() => setPokemonId(pokemonId - 1 > 0 ? pokemonId - 1 : 1)} 
          disabled={pokemonId === 1}>
          Previous
        </button>
        <button 
          onClick={() => setPokemonId(pokemonId + 1)}>
          Next
        </button>
      </div>
    </div>
  );
}