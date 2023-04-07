using UnityEngine;
using System.Collections;

public class Calculator : MonoBehaviour 
{
	private double a=0;
	private double b=0;
	private int operattionIndex = 0; // 1:+ , 2:- , 3:* , 4 ill write down
	private double result = 0;
	private double carryOver=0;
	private int currentEdit = 1; // 1= a && 2 = b && 0 = operator
	// lets keep a as the default entry point
	// chumma to keep track of which part the user is entering
	// example in a+b is he typing for a or b 
	// must look familiar right ? :P
	// very funny :P 
	// operationIndex :
	// tell which u didnt ? a min

	void Update()
	{
		// on a touch screen you have a different logic 
		// we have to detect touch position not mouse position 
		// first let me see if i can export to android before i change code

		if(Input.GetTouch(0))
		{	
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 100))  
			{
				if(hit.collider.gameObject.tag == "button")
				{ 	
					//hit.collider.transform.position = new 
						//Vector3(hit.collider.transform.position.x,hit.collider.transform.position.y,0.04f);
					buttonValue bv = hit.collider.gameObject.GetComponent<buttonValue>();
					string theValue = bv.value;
					GameObject theScreen = GameObject.FindGameObjectWithTag("Display");
					TextMesh screentext = (TextMesh)theScreen.GetComponent<TextMesh>();
					char val = theValue[0];
					// till here i hope you understo
					if(currentEdit == 0)
					{
						screentext.color = Color.red;
						Debug.Log("Operator :" +val);
						switch (val)
						{
							case '+' : operattionIndex = 1;break;
							case '-' : operattionIndex = 2;break;
							case '*' : operattionIndex = 3;break;
							case '/' : operattionIndex = 4;break;
						}
						currentEdit = 2;
						screentext.color = Color.yellow;
					}
					else if(currentEdit == 1)
					{
						screentext.color = Color.white;
						// we will store whatever the user keeps entering into variable a for currentEdit = 1
						if( val == '+' || val == '-' || val == '*' || val == '/')
						{
							currentEdit = 0;
						}
						else
						{
							a = a*10 + int.Parse(theValue);
							screentext.text = a.ToString();
						}
					}
					else if (currentEdit == 2)
					{
						screentext.color = Color.green;
						if( val == '=')
						{
							screentext.color = Color.black;
							switch(operattionIndex)
							{
							case 1: result = a+b;break;
							case 2: result = a-b;break;
							case 3: result = a*b;break;
							case 4: if(b != 0)
								result = a/b;
								else
									result = 0;
								break;
							}
							screentext.text = result.ToString();
							Debug.Log("opindex: "+operattionIndex);
							// ok the index is not being assigned :|
							// display result and reset calculcator
							a= 0;
							b = 0;
							currentEdit = 1;
						}
						else // ok all cool
						{
							screentext.text = "0";
							b = b*10 + int.Parse(theValue);
							screentext.text = b.ToString();
						}
					}
					//hit.collider.transform.position = new 
						//Vector3(hit.collider.transform.position.x,hit.collider.transform.position.y,0.44f);
				}
			}
		}
	}
}