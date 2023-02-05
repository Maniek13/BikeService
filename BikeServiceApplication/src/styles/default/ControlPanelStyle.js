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
    marginLeft: 10,
    flexDirection:'row', 
    flexWrap:'wrap',
    backgroundColor: 'white',
    borderTopLeftRadius: 10,
    borderBottomLeftRadius: 10
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
  },
  deleteBtn : {
    width: 20,
    backgroundColor: 'red',
    borderWidth: 1,
    borderTopRightRadius: 10,
    borderBottomRightRadius: 10,
    marginRight: 10,
    borderLeftWidth: 0
  },
  deleteTxt : {
    color: 'white',
    fontSize: 16,
    fontWeight: 'bold',
    textAlign: 'center'
  },
  itemConteiner: {
    padding: 10,
    display: 'flex',
    flexDirection: 'row',
    justifyContent: 'center'
  }
});

export default conjectural;