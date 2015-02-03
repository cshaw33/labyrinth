using UnityEngine;
using System.Collections;

public class CardDeckBehaviorScript : MonoBehaviour {


	//Assign cards IDs using CHARS.  
	//Legend
	/////////////////////////////////////////////////
	// a. Elvish Leaf (5) Doubles the roll on the dice.  Play before rolling.
	// b. Magic Flute (4) Lure an opponent to sleep.  They miss a turn.  Play at any time before turn has ended.
	// c. Mind Ring (8) Roll a 6 sided die.  If a 4, 5, or 6 is rolled, protects against an attack from an amulet or magic flute.
	// d. Fairy (4) At any point before your turn has ended, you may forfeit your remaining steps and roll again.
	// e. Gnome (5) Discard one of the cards from your hand to take two from an opponent.
	// f. Amulet (8) After the amulet has been played, the steps remaining are used to move an opponent.
	// g. Water Nymph Stone (10) Pass through a wall of foliage by turning it to vapor.
	// h. Star Glass (5) See an opponent's cards
	// i. Dragon (4) Fly a player to a quadrant of your choice.
	// j. Sword (6) Roll a 6 sided die.  If 4, 5, or 6 is rolled, you have fended off an attack from a Gnome or a Dragon.
	// k. Time Turner (5) Look at the top 5 cards on the discard pile and put one of them into your hand.

	private char[] deck;
	private int drawIndex;
	private char[] discard;
	private int discardIndex;

	private char[] player1Cards;
	private char[] player2Cards;
	private char[] player3Cards;
	private char[] player4Cards;


	// Use this for initialization
	void Start () {
		//There are 64 Cards in this deck.  That's super efficient for memory.
		deck = new char[64];
		drawIndex = 0;
		discard = new char[64];
		discardIndex = 0;

		player1Cards = new char[64];
		player2Cards = new char[64];
		player3Cards = new char[64];
		player4Cards = new char[64];

		initializeDeck();
		printDeck();
		shuffleDeck();
		print("deck has been shuffled: "+deck);
		printDeck();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void initializeDeck(){

		int i=0;

		deck[i] = 'a'; i++; deck[i] = 'a'; i++; deck[i] = 'a'; i++; deck[i] = 'a'; i++; deck[i] = 'a'; i++;
		deck[i] = 'b'; i++; deck[i] = 'b'; i++; deck[i] = 'b'; i++; deck[i] = 'b'; i++; 
		deck[i] = 'c'; i++; deck[i] = 'c'; i++; deck[i] = 'c'; i++; deck[i] = 'c'; i++; deck[i] = 'c'; i++; deck[i] = 'c'; i++; deck[i] = 'c'; i++; deck[i] = 'c'; i++; 
		deck[i] = 'd'; i++; deck[i] = 'd'; i++; deck[i] = 'd'; i++; deck[i] = 'd'; i++; 
		deck[i] = 'e'; i++; deck[i] = 'e'; i++; deck[i] = 'e'; i++; deck[i] = 'e'; i++; deck[i] = 'e'; i++; 
		deck[i] = 'f'; i++; deck[i] = 'f'; i++; deck[i] = 'f'; i++; deck[i] = 'f'; i++; deck[i] = 'f'; i++; deck[i] = 'f'; i++; deck[i] = 'f'; i++; deck[i] = 'f'; i++; 
		deck[i] = 'g'; i++; deck[i] = 'g'; i++; deck[i] = 'g'; i++; deck[i] = 'g'; i++; deck[i] = 'g'; i++; deck[i] = 'g'; i++; deck[i] = 'g'; i++; deck[i] = 'g'; i++; deck[i] = 'g'; i++; deck[i] = 'g'; i++; 
		deck[i] = 'h'; i++; deck[i] = 'h'; i++; deck[i] = 'h'; i++; deck[i] = 'h'; i++; deck[i] = 'h'; i++; 
		deck[i] = 'i'; i++; deck[i] = 'i'; i++; deck[i] = 'i'; i++; deck[i] = 'i'; i++; 
		deck[i] = 'j'; i++; deck[i] = 'j'; i++; deck[i] = 'j'; i++; deck[i] = 'j'; i++; deck[i] = 'j'; i++; deck[i] = 'j'; i++; 
		deck[i] = 'k'; i++; deck[i] = 'k'; i++; deck[i] = 'k'; i++; deck[i] = 'k'; i++; deck[i] = 'k'; i++; 

	}

	void shuffleDeck(){

		int rand1 = 0;
		int rand2 = 0;
		for(int i=0; i<300; i++){
			rand1 = Random.Range(0, 64);
			rand2 = Random.Range(0, 64);

			if(rand1 == rand2) rand2 = Random.Range(0,64);

			char card1 = deck[rand1];
			char card2 = deck[rand2];

			deck[rand1] = card2;
			deck[rand2] = card1;

		}
		bubbleUp();
	}

	void printDeck(){
		string deckString = "";
		for(int i=0; i<64; i++){
			deckString = deckString + deck[i];
		}
		print(deckString);
	}

	void shuffleDiscardPile() {
		deck = discard;
		discard = new char[64];
		shuffleDeck();
	}

	void bubbleUp() {
		//if there are any cards removed from the deck when it is shuffled, bubble those cards to the top and reset the drawIndex.

		int bubbleIndex = 0;
		int endIndex = deck.Length - 1;
		print("End index = "+ endIndex);

		bubbleRecursion(bubbleIndex, endIndex);

	}

	void bubbleRecursion(int bubbleIndex, int endIndex){
		printDeck();
		if(bubbleIndex  == endIndex) return;
		if(deck[bubbleIndex] == '0'){
			bubbleRecursion (bubbleIndex + 1, endIndex);
		}
		else{
			if(deck[endIndex] == '0'){
				swapCards(bubbleIndex, endIndex);
				bubbleRecursion(bubbleIndex + 1, endIndex - 1);
			}
			else{
				bubbleRecursion(bubbleIndex, endIndex - 1);
			}
		}
	}

	void swapCards(int index1, int index2){
		char char1 = deck[index1];
		char char2 = deck[index2];
		deck[index1] = char2;
		deck[index2] = char1;
	}

	char drawCardFromDeck() {
		char card = deck[drawIndex];
		deck[drawIndex] = '0';
		drawIndex++;

		if(drawIndex > 63) {
			shuffleDiscardPile();
		}

		return card;

	}

	void discardCard(char card){
		discard[discardIndex] = card;
		discardIndex++;
	}

	char drawCardFromDiscard() {
		if(discardIndex == 0){
			return '0';
		}

		discardIndex--;
		char card = discard[discardIndex];
		discard[discardIndex] = '0';
		return card;
	}
}
