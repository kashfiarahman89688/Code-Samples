public class MatrixRotationSpiral{
	public static int m_r= 5/2;
	public static int m_c = 5/2;
	public static void main(String[] args){
		for(int i=0; i<5; i++ ){
			for(int j=0; j<5; j++){
				System.out.print("* ("+i+","+j+")");
			}
			System.out.println();
		}

		System.out.println(m_r+" "+m_c);
		m_c = m_c+1;

		for(int k = 1; k<= 5; k=k+2){
			
				//print right to right dia
				System.out.println("row: " +m_r+ "col: "+m_c);
				rightToRightUp(m_r,m_c,k);
				//print right dia up to left dia (up)
				System.out.println("row: " +m_r+ "col: "+m_c);
				rightUpToLeftUp(m_r,m_c,k);
				//print left dia up to left dia down
				System.out.println("row: " +m_r+ "col: "+m_c);
				rightUpToRightDown(m_r,m_c,k);
				//print left dia down to right dia down
				System.out.println("row: " +m_r+ "col: "+m_c);
				leftDowntoRightDown(m_r,m_c,k);
				if(m_c >= 4 && m_r >= 4)
					break;

				System.out.println("----------------");
			
		}
	}

	public static void rightToRightUp(int row, int col,int i){
		System.out.println(i+" "+row+" "+col);
		int j;
		for(j=row-1; j>=row-i; j--)
			System.out.println(i+" "+j+" "+col);
		m_r= j+1;
		m_c = col-1;
	}

	public static void rightUpToLeftUp(int row,int col, int i){
		int k;
		for(k = col; k>=col-i ; k--)
			System.out.println(i+" "+row+" "+k);
		m_r = row+1;
		m_c = k+1;
	}

	public static void rightUpToRightDown(int row,int col, int i){
		int k;
		for(k=row; k<=row+i; k++)
			System.out.println(i+" "+k+" "+col);
		m_r= k-1;
		m_c = col+1;


	}

	public static void leftDowntoRightDown(int row,int col, int i){
		int k;
		for(k=col;k<=col+i; k++)
			System.out.println(i+" "+row+" "+k);	
		m_r = row;
		m_c = k; 
	}
}