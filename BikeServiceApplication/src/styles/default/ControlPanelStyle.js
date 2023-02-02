import {StyleSheet} from 'react-native';

const conjectural = StyleSheet.create({
  flatList : {
    marginTop: 10
  },
  textList : {
    height: 20
  },
  listItem : {
    borderWidth: 1,
    marginBottom: 5,
    marginLeft: 10,
    marginRight: 10,
    flexDirection:'row', 
    flexWrap:'wrap',
    backgroundColor: 'white',
    borderRadius: 10
  },
  buttonText : {
    color: 'white',
    textAlign: 'center',
    fontSize: 30,
    fontWeight: 'bold'
  },
  searchBtn : {
    width: '33%'
  },
  searchBtnText : {
    textAlign: 'center',
    color: 'black'
  }
});

export default conjectural;