Structure 	        Operation 	                    Big-O (Avg) One-sentence rationale
--------------------------------------------------------------------------------------
Array 	            Access by index 		        O(1) as it has direct access
Array 	            Search (unsorted) 		        O(n) as it does a linear search
List<T> 	        Add at end 		                O(1) average per add is constant
List<T> 	        Insert at index 		        O(n) has to shift tail elements
Stack<T> 	        Push / Pop / Peek 		        O(1) only has to operate at the ends
Queue<T> 	        Enqueue / Dequeue / Peek 		O(1) only has to operate at the ends
Dictionary<K,V> 	Add / Lookup / Remove 		    O(1) deals with keys
HashSet<T> 	        Add / Contains / Remove 	    O(1) uniqueness is enforced